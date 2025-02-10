using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Realtime.Domain.Entity;
using Realtime.Infrastructure.DTOs;

namespace Realtime.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        Task<AuthenicationResponse> CreateJwtToken(ApplicationUser user);
        Task<ClaimsPrincipal> GetPrincipalFromToken(string Token);
        Task<JwtSecurityToken> GenerateResetPasswordJwtToken(ApplicationUser user);
    }
}