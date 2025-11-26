using System.ComponentModel.DataAnnotations.Schema;

[Table("users")]
public class User
{
	[Column("id")]
	public int Id { get; set; }

	[Column("username")]
	public string Username { get; set; } = "";

	[Column("email")]
	public string Email { get; set; } = "";

	[Column("password_hash")]
	public string PasswordHash { get; set; } = "";
}