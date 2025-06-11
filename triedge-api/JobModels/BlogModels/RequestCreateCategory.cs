using System.ComponentModel.DataAnnotations;

namespace triedge_api.JobModels.BlogModels;

public record class RequestCreateCategory
{
    [Required]
    public string Name { get; set; } = null!;
}
