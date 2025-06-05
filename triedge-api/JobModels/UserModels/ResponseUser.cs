namespace triedge_api.JobModels.UserModels;

public record class ResponseUser : ResponseEntity
{
    public string Login { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Avatar { get; set; }
}
