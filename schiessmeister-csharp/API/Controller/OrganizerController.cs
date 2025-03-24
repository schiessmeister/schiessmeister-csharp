using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controller;

[Authorize(Roles = "User")]
[ApiController]
[Route("api/organizers")]
public class OrganizerController : ControllerBase {
    private readonly IOrganizerRepository _repository;

    public OrganizerController(IOrganizerRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Organizer>>> GetAll() {
        return Ok((await _repository.FindAllAsync()).Select(o => new OrganizerResponseDTO() {
            Id = o.Id,
            name = o.name,
        }));
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("{id}")]
    public async Task<ActionResult<Organizer>> GetOrganizer(int id) {
        var foundObject = await _repository.FindByIdAsync(id);
        if (foundObject == null)
            return NotFound();

        return Ok(
            new OrganizerResponseDTO() {
                Id = foundObject.Id,
                name = foundObject.name,
            }
        );
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Organizer>> CreateOrganizer(OrganizerCreateDTO newOrganizerDTO) {
        var newOrganizer = new Organizer() {
            name = newOrganizerDTO.name
        };
        var createOrganizer = await _repository.AddAsync(newOrganizer);
        return CreatedAtAction(nameof(GetOrganizer), new { id = createOrganizer.Id }, new OrganizerResponseDTO() {
            Id = createOrganizer.Id,
            name = createOrganizer.name,
        });
    }

    [HttpPut]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Organizer>> UpdateOrganizer(int id, OrganizerUpdateDTO updatedOrganizer) {
        var existingOrganizer = await _repository.FindByIdAsync(id);

        if (existingOrganizer == null)
            return NotFound();

        existingOrganizer.name = updatedOrganizer.name;
        await _repository.UpdateAsync(existingOrganizer);

        return Ok(new OrganizerResponseDTO() {
            Id = existingOrganizer.Id,
            name = existingOrganizer.name
        });
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Organizer>> DeleteOrganizer(int id) {
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
        if (organizer == null)
            return NotFound();

        var competitions = organizer.Competitions ?? new List<Competition>();

        return Ok(competitions);
    }
}