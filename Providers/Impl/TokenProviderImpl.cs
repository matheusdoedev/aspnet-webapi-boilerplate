using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace AspNetWebApiBoilerplate.Providers;

public class TokenProviderImpl : ITokenProvider
{
	public JwtSecurityToken GenerateAccessToken(IEnumerable<Claim> claims)
	{
		throw new NotImplementedException();
	}

	public string GenerateRefreshToken()
	{
		throw new NotImplementedException();
	}

	public ClaimsPrincipal GetPrincipalFromExpiredToken(string token, IConfiguration _config)
	{
		throw new NotImplementedException();
	}
}