using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using schiessmeister_csharp.Domain.Models.Auth;
using schiessmeister_csharp.Domain.Models;

namespace schiessmeister_csharp.API.Services;

public class JwtService : ITokenService {
    private readonly UserManager<AppUser> userManager;
    private readonly IConfiguration configuration;

    public JwtService(UserManager<AppUser> userManager, IConfiguration configuration) {
        this.userManager = userManager;
        this.configuration = configuration;
    }

    public async Task<TokenDTO> CreateTokenAsync(AppUser user) {
        List<Claim> authClaims = [
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];

        IList<UserRole> userRoles = (IList<UserRole>)await userManager.GetRolesAsync(user);
        foreach (UserRole userRole in userRoles) {
            authClaims.Add(new Claim(ClaimTypes.Role, userRole.ToString()));
        }

        var authSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration.GetSection("JwtSettings")["Secret"]!));

        var token = new JwtSecurityToken(
            expires: DateTime.Now.AddDays(15),
            claims: authClaims,
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );

        return new TokenDTO() {
            token = new JwtSecurityTokenHandler().WriteToken(token),
            expiration = token.ValidTo,
            roles = userRoles,
            id = user.Id.ToString()
        };
    }
}