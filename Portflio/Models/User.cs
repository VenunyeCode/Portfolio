namespace Portflio.Models;

public class User
{
    public int Id { get; set; }
    public String Email { get; set; } = String.Empty;
    public String Username { get; set; } = String.Empty;
    public String Password { get; set; } = String.Empty;
}