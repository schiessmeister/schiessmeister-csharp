using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Models.Auth;

namespace schiessmeister_csharp.API.Services;

public interface ITokenService {

    public Task<TokenDTO> CreateTokenAsync(AppUser user);
}