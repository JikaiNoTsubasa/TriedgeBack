namespace triedge_api.JobModels.AuthModel;

public record class ResponseLogin
{
    public string Token { get; set; } = null!;
}
