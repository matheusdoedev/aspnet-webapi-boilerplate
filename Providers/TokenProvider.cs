using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspNetWebApiBoilerplate.Providers;

public interface ITokenProvider
{
    JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims);
    string GenerateRefreshToken();
    ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config);
}