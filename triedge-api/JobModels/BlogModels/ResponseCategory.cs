namespace triedge_api.JobModels.BlogModels;

public record class ResponseCategory : ResponseEntity
{
    public string Name { get; set; } = null!;
}
