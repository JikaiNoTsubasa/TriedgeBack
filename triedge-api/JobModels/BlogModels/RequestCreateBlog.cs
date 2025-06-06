using System.ComponentModel.DataAnnotations;

namespace triedge_api.JobModels.BlogModels;

public record class RequestCreateBlog
{
    [Required]
    public string Title { get; set; } = null!;
    [Required]
    public string Content { get; set; } = null!;
    public string? Image { get; set; }
}
