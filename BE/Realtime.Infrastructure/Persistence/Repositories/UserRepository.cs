using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.DTOs;
using Realtime.Infrastructure.DTOs.Auth;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IRealtimeDbContext _db;
        private readonly IJwtService _jwtService;
        public UserRepository(IConfiguration config, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IRealtimeDbContext db, IJwtService jwtService)
        {
            _config = config;
            _userManager = userManager;
            _db = db;
            _jwtService = jwtService;
            _signInManager = signInManager;
        }
        public async Task<AuthenicationResponse> Login(LoginDto loginDto)
        {
            var checkUser = await _userManager.FindByNameAsync(loginDto.UserName);
            if (checkUser == null) throw new Exception("User not found !");
            var checkpw = await _signInManager.PasswordSignInAsync(checkUser.UserName, loginDto.Password, isPersistent: false, lockoutOnFailure: false);
            if (checkpw.Succeeded)
            {
                var response = await _jwtService.CreateJwtToken(checkUser);
                var refreshtoken = _db.RefreshTokens.Where(x => x.ExpirationDate <= DateTime.UtcNow).ToList();
                if (refreshtoken.Count > 0)
                {
                    _db.RefreshTokens.RemoveRange(refreshtoken);
                }
                var newRefreshToken = response.RefreshToken;
                await _db.RefreshTokens.AddAsync(new RefreshToken()
                {
                    Token = newRefreshToken,
                    UserId = checkUser.Id,
                    CreatedDate = DateTime.UtcNow,
                    ExpirationDate = DateTime.UtcNow.AddDays(Double.Parse(_config["RefreshToken:ExpirationInDays"]))
                });
                await _db.SaveChangesAsync();
                return response;
            }
            else
            {
                throw new Exception("username or password is incorrect !");
            }
        }

        public async Task<AuthenicationResponse> Register(RegisterDTO registerDTO)
        {
            var emailCheck = await _userManager.FindByEmailAsync(registerDTO.Email);
            if (emailCheck != null)
            {
                throw new Exception("Email already exists");
            }

            var user = new ApplicationUser()
            {
                Email = registerDTO.Email,
                PhoneNumber = registerDTO.PhoneNumber,
                UserName = registerDTO.Username,
                SecurityStamp = Guid.NewGuid().ToString(),
            };

            var result = await _userManager.CreateAsync(user, password: registerDTO.Password);
            if (!result.Succeeded)
            {
                string errorMessage = string.Join(" | ", result.Errors.Select(x => x.Description));
                throw new Exception(errorMessage);
            }

            // Thêm vai trò
            await _userManager.AddToRoleAsync(user, "User");

            // Đăng nhập người dùng
            await _signInManager.SignInAsync(user, isPersistent: false);

            // Tạo JWT token
            var response = await _jwtService.CreateJwtToken(user);

            // Xóa refresh token cũ
            var refreshToken = _db.RefreshTokens.Where(x => x.ExpirationDate <= DateTime.UtcNow).ToList();
            if (refreshToken.Count > 0)
            {
                _db.RefreshTokens.RemoveRange(refreshToken);
            }

            // Tạo refresh token mới
            var newRefreshToken = new RefreshToken
            {
                Token = response.RefreshToken,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(Double.Parse(_config["RefreshToken:ExpirationInDays"]))
            };
            await _db.RefreshTokens.AddAsync(newRefreshToken);
            await _db.SaveChangesAsync();

            return response;
        }

        public async Task<string> Logout(RefreshTokenDTO refreshTokenDTO)
        {
            var claims = await _jwtService.GetPrincipalFromToken(refreshTokenDTO.Token);
            var refreshtoken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshTokenDTO.Token);
            if (refreshtoken == null) throw new Exception("Refresh token not found !");
            var UserId = claims.Claims.Single(x => x.Type == "uid").Value;
            if (UserId != refreshtoken.UserId.ToString()) throw new Exception("Invalid refresh token !");
            _db.RefreshTokens.Remove(refreshtoken);
            await _db.SaveChangesAsync();
            return "Logout successfully !";
        }

        public async Task<AuthenicationResponse> RefreshToken(RefreshTokenDTO refreshTokenDTO)
        {
            var payload = await _jwtService.GetPrincipalFromToken(refreshTokenDTO.Token);
            var refreshToken = await _db.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshTokenDTO.Token);
            if (refreshToken is null) throw new Exception("Token is not found or expired.");
            var user = await _userManager.FindByIdAsync(payload.Claims.Single(x => x.Type == "uid").Value);
            if (user is null) throw new Exception("Invalid Token");
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            var response = await _jwtService.CreateJwtToken(user);
            _db.RefreshTokens.Remove(refreshToken);
            await _db.RefreshTokens.AddAsync(new RefreshToken()
            {
                Token = response.RefreshToken,
                UserId = user.Id,
                CreatedDate = DateTime.UtcNow,
                ExpirationDate = DateTime.UtcNow.AddDays(Double.Parse(_config["RefreshToken:ExpirationInDays"]))
            });
            await _db.SaveChangesAsync();
            return response;
        }
    }
}