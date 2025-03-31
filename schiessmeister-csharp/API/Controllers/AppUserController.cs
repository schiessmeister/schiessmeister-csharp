using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(Roles = "User")]
[ProducesResponseType(StatusCodes.Status401Unauthorized)]
[ProducesResponseType(StatusCodes.Status403Forbidden)]
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
    public async Task<ActionResult<AppUser>> GetUser(int id) {
        var organizer = await _users.FindByIdAsync(id);

        if (organizer == null)
            return NotFound();

        return Ok(organizer);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppUser>> DeleteUser(int id) {
        var existingOrganizer = await _users.FindByIdAsync(id);
        if (existingOrganizer == null)
            return NotFound();

        await _users.DeleteAsync(existingOrganizer);

        return Ok();
    }

    [HttpGet]
    [Route("{id}/competitions")]
    [Authorize(Roles = "Organizer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitionsByOrganizer(int id) {
        return Ok(await _competitions.FindByOrganizerIdAsync(id));
    }
}