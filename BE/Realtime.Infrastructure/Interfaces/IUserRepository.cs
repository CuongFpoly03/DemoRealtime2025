using Realtime.Infrastructure.DTOs;
using Realtime.Infrastructure.DTOs.Auth;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IUserRepository
    {
        Task<AuthenicationResponse> Register(RegisterDTO registerDTO);
        Task<AuthenicationResponse> Login(LoginDto loginDto);
        Task<string> Logout(RefreshTokenDTO refreshTokenDTO);
         Task<AuthenicationResponse> RefreshToken(RefreshTokenDTO refreshTokenDTO);
    }
}