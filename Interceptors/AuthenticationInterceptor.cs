using Microsoft.AspNetCore.Mvc;

[Controller]
[Route("api/auth")]
public class AuthenticationInterceptor(AuthenticationPort authenticationPort)
{
	private readonly AuthenticationPort _authenticationPort = authenticationPort;

	[HttpPost("login")]
	public IResult PostLogin([FromBody] LoginDto loginDto)
	{
		try
		{
			LoginResponseDto response = _authenticationPort.Login(loginDto);

			return Results.Ok(response);
		}
		catch
		{
			throw;
		}
	}
}