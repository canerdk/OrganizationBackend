using Entities.Dtos.Auth;
using System.Security.Claims;

namespace Business.Utilities.Security.JWT
{
    public interface IJwtManager
    {
        string GenerateJwtToken(UserDto user);
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
        string GenerateRefreshToken();
    }
}
