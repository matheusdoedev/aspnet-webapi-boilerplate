using Microsoft.AspNetCore.Mvc;

[Controller]
[Route("api/auth")]
public class AuthenticationInterceptor(AuthenticationPort authenticationPort)
{
	private readonly AuthenticationPort _authenticationPort = authenticationPort;

	[HttpPost("login")]
	public async Task<IResult> PostLogin([FromBody] LoginDto loginDto)
	{
		try
		{
			LoginResponseDto response = await _authenticationPort.Login(loginDto);

			return Results.Ok(response);
		}
		catch
		{
			throw;
		}
	}
}