using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.API.Extensions;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Roles = "User")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
public class AppUserController : ControllerBase {
    private readonly IAppUserRepository _users;
    private readonly ICompetitionRepository _competitions;

    public AppUserController(IAppUserRepository users, ICompetitionRepository competitions) {
        _users = users;
        _competitions = competitions;
    }

    [HttpGet]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AppUser>> GetUser(int id) {
        int currentUserId = User.GetUserId();
        if (currentUserId != id)
            return Forbid();

        var user = await _users.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<AppUser>> DeleteUser(int id) {
        int currentUserId = User.GetUserId();
        if (currentUserId != id)
            return Forbid();

        var user = await _users.FindByIdAsync(id);
        if (user == null)
            return NotFound();

        await _users.DeleteAsync(user);

        return Ok();
    }

    [HttpGet]
    [Route("{id}/competitions")]
    [Authorize(Roles = "Organizer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitionsByOrganizer(int id) {
        int currentUserId = User.GetUserId();
        if (currentUserId != id)
            return Forbid();

        return Ok(await _competitions.FindByOrganizerIdAsync(id));
    }
}