using schiessmeister_csharp.API.Services;
using schiessmeister_csharp.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models.Auth;

namespace schiessmeister_csharp.API.Controller;

[Route("api/[controller]")]
[ApiController]
public class AuthenticateController : ControllerBase {
    private readonly UserManager<ApplicationUser> userManager;
    private readonly ITokenService tokenService;

    public AuthenticateController(UserManager<ApplicationUser> userManager, IConfiguration _, ITokenService tokenService) {
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

        ApplicationUser user = new ApplicationUser() {
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