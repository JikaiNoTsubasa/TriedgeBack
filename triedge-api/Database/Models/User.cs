namespace triedge_api.Database.Models;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
}