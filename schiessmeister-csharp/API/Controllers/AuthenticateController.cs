using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models.Auth;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Services;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/authenticate")]
public class AuthenticateController : ControllerBase {
    private readonly UserManager<AppUser> userManager;
    private readonly ITokenService tokenService;

    public AuthenticateController(UserManager<AppUser> userManager, IConfiguration _, ITokenService tokenService) {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    [HttpPost]
    [Route("login")]
    public async Task<ActionResult<TokenDTO>> Login([FromBody] LoginDTO model) {
        var user = await userManager.FindByNameAsync(model.Username);

        if (user != null && await userManager.CheckPasswordAsync(user, model.Password)) {
            return Ok(await tokenService.CreateTokenAsync(user));
        }

        return Unauthorized();
    }

    [HttpPost]
    [Route("register")]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterDTO model) {
        var userExists = await userManager.FindByNameAsync(model.Username);
        if (userExists != null)
            return BadRequest("User already exists!");

        AppUser user = new AppUser() {
            Email = model.Email,
            SecurityStamp = Guid.NewGuid().ToString(),
            UserName = model.Username
        };

        IdentityResult result = await userManager.CreateAsync(user, model.Password);

        if (result.Errors.Count() != 0)
            return BadRequest(result.Errors);

        foreach (var er in result.Errors) {
            Console.WriteLine(er.Description);
        }

        if (!result.Succeeded)
            return BadRequest("User creation failed! Please check user details and try again.");

        await userManager.AddToRoleAsync(user, "User");

        return Ok("User created successfully!");
    }
}