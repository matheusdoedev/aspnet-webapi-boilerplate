using Microsoft.AspNetCore.Mvc;

namespace AspnetWebApiBoilerplate.Controllers;

[ApiController]
[Route("")]
public class HealthCheckController {
	[HttpGet]
	public string GetHealthCheck() {
		return DateTime.UtcNow.ToLongDateString();
	}
}