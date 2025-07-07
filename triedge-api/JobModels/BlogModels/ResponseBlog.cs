using triedge_api.Database.Models;
using triedge_api.JobModels.UserModels;

namespace triedge_api.JobModels.BlogModels;

public record class ResponseBlog : ResponseEntity
{
    public string Title { get; init; } = null!;
    public string Content { get; init; } = null!;
    public ResponseUser Owner { get; set; } = null!;
    public DateTime? PublishedDate { get; set; }
    public string? Image { get; set; }
    public string Slug { get; set; } = null!;
    public List<ResponseCategory>? Categories { get; set; }
    public BlogStatus Status { get; set; }
    public int Viewed { get; set; }
}
