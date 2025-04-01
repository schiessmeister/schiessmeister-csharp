using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.API.Extensions;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/competition")]
public class CompetitionController : ControllerBase {
    private readonly ICompetitionRepository _competitions;

    public CompetitionController(ICompetitionRepository competitions) {
        _competitions = competitions;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetAll() {
        return Ok(await _competitions.FindAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Competition>> Get(int id) {
        var comp = await _competitions.FindByIdFullAsync(id);

        if (comp == null)
            return NotFound();

        return Ok(comp);
    }

    [HttpPost]
    [Authorize(Roles = "Organizer")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Competition>> Post(Competition comp) {
        if (User.GetUserId() != comp.OrganizerId)
            return Forbid();

        var newComp = await _competitions.AddAsync(comp);

        return CreatedAtAction(nameof(Get), new { id = newComp.Id }, newComp);
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Organizer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<Competition>> Put(int id, Competition comp) {
        if (User.GetUserId() != comp.OrganizerId)
            return Forbid();

        comp.Id = id;

        return Ok(await _competitions.UpdateAsync(comp));
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Organizer")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Delete(int id) {
        var comp = await _competitions.FindByIdAsync(id);
        if (comp == null)
            return NotFound();

        if (User.GetUserId() != comp.OrganizerId)
            return Forbid();

        await _competitions.DeleteAsync(comp);

        return Ok();
    }

    [HttpGet("{id}/subscribe")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> GetSubscriptionInfo(int id) {
        var comp = await _competitions.FindByIdAsync(id);

        if (comp == null)
            return NotFound();

        // Return connection info for the client
        return Ok(new {
            hubUrl = "/hubs/competition",
            competitionId = id,
            methodName = "SubscribeToCompetition",
            eventName = "CompetitionUpdated"
        });
    }
}