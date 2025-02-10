using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.DTOs;
using Realtime.Infrastructure.Interfaces;

namespace Realtime.Infrastructure.services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _config;
        private readonly UserManager<ApplicationUser> _userManager;

        public JwtService(IConfiguration config, UserManager<ApplicationUser> userManager)
        {
            _config = config;
            _userManager = userManager;
        }

        public async Task<AuthenicationResponse> CreateJwtToken(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var isActived = user.LockoutEnabled;
            var jwtToken = await GenerateJwtToken(user);
            var jwtRefreshToken = await GenerateRefreshToken(user);
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            string refreshToken = new JwtSecurityTokenHandler().WriteToken(jwtRefreshToken);
            return new AuthenicationResponse
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Token = token,
                RefreshToken = refreshToken,
                LockoutEnabled = isActived
            };
        }

        public async Task<JwtSecurityToken> GenerateJwtToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationInMinutes"]));
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }


            var userName = user.UserName ?? throw new ArgumentNullException(nameof(user.UserName), "UserName cannot be null.");
            var userEmail = user.Email ?? "";
            var userId = user.Id != Guid.Empty ? user.Id.ToString() : throw new ArgumentNullException(nameof(user.Id), "User ID cannot be null.");


            var claims = new[]
            {
    new Claim(JwtRegisteredClaimNames.Sub, userName),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim(JwtRegisteredClaimNames.Email, userEmail),
    new Claim("uid", userId)
}
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Secret"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials);
            return tokenGenerator;
        }

        public async Task<JwtSecurityToken> GenerateResetPasswordJwtToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpirationResetPasswordInMinutes"]));
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);

            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user), "User cannot be null.");
            }


            var userName = user.UserName ?? throw new ArgumentNullException(nameof(user.UserName), "UserName cannot be null.");
            var userEmail = user.Email ?? "";
            var userId = user.Id != Guid.Empty ? user.Id.ToString() : throw new ArgumentNullException(nameof(user.Id), "User ID cannot be null.");


            var claims = new[]
            {
    new Claim(JwtRegisteredClaimNames.Sub, userName),
    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
    new Claim(JwtRegisteredClaimNames.Email, userEmail),
    new Claim("uid", userId)
}
            .Union(userClaims)
            .Union(roleClaims);

            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["Jwt:Secret"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: expiration,
                signingCredentials: signingCredentials);
            return tokenGenerator;
        }

        public async Task<JwtSecurityToken> GenerateRefreshToken(ApplicationUser user)
        {
            var expiration = DateTime.UtcNow.AddDays(Convert.ToDouble(_config["RefreshToken:ExpirationInDays"]));
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var userEmail = user.Email ?? "";
            var roleClaims = new List<Claim>();

            for (int i = 0; i < roles.Count; i++)
            {
                roleClaims.Add(new Claim("roles", roles[i]));
            }
            var claims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, userEmail),
                new Claim("uid",user.Id.ToString())
            }
            .Union(userClaims)
            .Union(roleClaims);
            SymmetricSecurityKey securityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["RefreshToken:Secret"]));
            SigningCredentials signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken tokenGenerator = new JwtSecurityToken(_config["RefreshToken:Issuer"], _config["RefreshToken:Audience"], claims, expires: expiration, signingCredentials: signingCredentials);
            return tokenGenerator;
        }

        public async Task<ClaimsPrincipal> GetPrincipalFromToken(string Token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_config["RefreshToken:Secret"] ?? "S3cr4etK3y")),
                ValidateLifetime = false
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(Token, tokenValidationParameters, out securityToken);
            var jwtSecurityToken = securityToken as JwtSecurityToken;
            if (jwtSecurityToken is null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Invalid Token");
            return principal;
        }
    }
}