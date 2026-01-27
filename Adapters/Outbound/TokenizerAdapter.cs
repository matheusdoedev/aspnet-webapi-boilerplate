
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using Microsoft.IdentityModel.Tokens;

public class TokenizerAdapter : TokenizerPort
{
	public string GenerateToken(Dictionary<string, string> claimsData)
	{
		Claim[] claims = new[]
		{
			new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
		};

		foreach (KeyValuePair<string, string> item in claimsData.ToList())
		{
			claims.Append(new Claim(item.Key, item.Value));
		}

		string secretKey = Environment.GetEnvironmentVariable("TOKEN_KEY") ?? throw new ArgumentException("Invalid token key.");
		string issuer = Environment.GetEnvironmentVariable("TOKEN_ISSUER") ?? throw new ArgumentException("Invalid token issuer.");
		int expiresAtInMinutes = int.Parse(Environment.GetEnvironmentVariable("TOKEN_EXPIRES_AT_IN_MINUTES") ?? "14400");

		SymmetricSecurityKey? securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
		SigningCredentials? credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
		JwtSecurityToken? token = new JwtSecurityToken(
							issuer: issuer,
							claims: claims,
							expires: DateTime.Now.AddMinutes(expiresAtInMinutes),
							signingCredentials: credentials
						);

		return new JwtSecurityTokenHandler().WriteToken(token);
	}
}