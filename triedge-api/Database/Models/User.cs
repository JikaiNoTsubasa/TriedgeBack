using SBIDotnetUtils.Security;
namespace triedge_api.Database.Models;

public class User : Entity
{
    public string Login { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Avatar { get; set; }

    public void SetPassword(string password) => Password = HashService.HashPassword(password);

    public List<Blog>? Blogs { get; set; }
    public bool CanLogin { get; set; } = true;
    public DateTime? LastConnection { get; set; }

}