namespace WebServiceToken.Models;

public class CreateUserModel
{
    public string Name { get; set; } = String.Empty;
    public string Username { get; set; } = String.Empty;
    public string Password { get; set; } = String.Empty;
    public string Role { get; set; } = "User";
}
