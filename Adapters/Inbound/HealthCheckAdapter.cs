
public class HealthCheckAdapter : HealthCheckPort
{
	public string CheckHealth()
	{
		return DateTime.Now.ToString();
	}
}