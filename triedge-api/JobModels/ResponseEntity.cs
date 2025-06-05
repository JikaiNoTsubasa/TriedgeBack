namespace triedge_api.JobModels;

public record class ResponseEntity
{
    public long Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
}
