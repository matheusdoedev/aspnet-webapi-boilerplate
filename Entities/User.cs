using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AspNetWebApiBoilerplate.Entities;

[Table("users")]
public class User
{
  [Key]
  public string Id { get; set; } = "";
  public string Username { get; set; } = "";
  public string Email { get; set; } = "";
  public string Password { get; set; } = "";
}
