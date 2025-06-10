namespace triedge_api.JobModels.AuthModel;

public record class RequestLogin
{
    public string? Login { get; set; }
    public string? Password { get; set; }
}
