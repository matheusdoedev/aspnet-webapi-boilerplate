using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
	public DbSet<User> Users { get; set; }

	public AppDbContext(DbContextOptions options) : base(options)
	{
		AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
		AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("DB_CONNECTION"));
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.UseDatabaseTemplate("postgres");
	}
}