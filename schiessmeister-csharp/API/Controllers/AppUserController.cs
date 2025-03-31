using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/users")]
public class AppUserController : ControllerBase {
    private readonly IAppUserRepository _repository;

    public AppUserController(IAppUserRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<AppUser>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult<AppUser>> GetUser(int id) {
        var organizer = await _repository.FindByIdAsync(id);

        if (organizer == null)
            return NotFound();

        return Ok(organizer);
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppUser>> UpdateUser(int id, AppUser organizer) {
        var existingOrganizer = await _repository.FindByIdAsync(id);

        if (existingOrganizer == null || existingOrganizer.OrganizerProfile == null)
            return NotFound();

        existingOrganizer.UserName = organizer.UserName;
        await _repository.UpdateAsync(existingOrganizer);

        return Ok(existingOrganizer);
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<AppUser>> DeleteOrganizer(int id) {
        var existingOrganizer = await _repository.FindByIdAsync(id);
        if (existingOrganizer == null)
            return NotFound();

        await _repository.DeleteAsync(existingOrganizer);

        return Ok();
    }

    [HttpGet]
    [Route("{id}/competitions")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetCompetitionsByOrganizer(int id) {
        var organizer = await _repository.FindByIdAsync(id);
        if (organizer == null || organizer.OrganizerProfile == null)
            return NotFound();

        List<Competition> competitions = organizer.OrganizerProfile.Competitions ?? [];

        return Ok(competitions);
    }
}