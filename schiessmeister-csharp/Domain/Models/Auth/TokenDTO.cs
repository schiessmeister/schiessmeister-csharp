namespace schiessmeister_csharp.Domain.Models.Auth;

public class TokenDTO {
    public string token { get; internal set; }
    public string id { get; internal set; }
    public DateTime expiration { get; internal set; }
    public IList<UserRole> roles { get; internal set; }
}