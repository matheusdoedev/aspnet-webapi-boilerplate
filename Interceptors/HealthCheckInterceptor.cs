using Microsoft.AspNetCore.Mvc;

[Controller]
[Route("")]
public class HealthCheckInterceptor(HealthCheckPort healthCheckPort)
{
	private readonly HealthCheckPort _healthCheckPort = healthCheckPort;

	[HttpGet]
	public IResult GetCheckHealth()
	{
		string currentTime = _healthCheckPort.CheckHealth();

		return Results.Ok(currentTime);
	}
}