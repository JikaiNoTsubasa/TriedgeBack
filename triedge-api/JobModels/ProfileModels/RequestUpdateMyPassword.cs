using System.ComponentModel.DataAnnotations;

namespace triedge_api.JobModels.ProfileModels;

public record class RequestUpdateMyPassword
{
    [Required]
    public string Password { get; set; } = null!;
}
