namespace triedge_api.JobModels.BlogModels;

public record class RequestUpdateBlog
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Image { get; set; }
    public List<long>? CategoryIds { get; set; }
}
