using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Identity;

namespace schiessmeister_csharp.API.Services;

public interface ITokenService {
        public Task<TokenDTO> CreateTokenAsync(ApplicationUser user);
}