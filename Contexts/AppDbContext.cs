using Microsoft.EntityFrameworkCore;

namespace AspNetWebApiBoilerplate.Contexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
		if (!optionsBuilder.IsConfigured)
			optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"));
	}
}