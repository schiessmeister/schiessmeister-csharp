using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;
using System.Runtime.InteropServices;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/competition")]
public class CompetitionController : ControllerBase {
    private readonly ICompetitionRepository _repository;
    private readonly IAppUserRepository _userRepository;

    public CompetitionController(ICompetitionRepository repository, IAppUserRepository userRepository) {
        _repository = repository;
        _userRepository = userRepository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Competition>> Get(int id) {
        var comp = await _repository.FindByIdAsync(id);

        if (comp == null)
            return NotFound();

        return Ok(comp);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Competition>> Post(Competition comp) {
        var newComp = await _repository.AddAsync(comp);

        return CreatedAtAction(nameof(Get), new { id = newComp.Id }, newComp);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Competition>> Put(int id, Competition competition) {
        var oldCompetition = await _repository.FindByIdAsync(id);

        if (oldCompetition == null)
            return NotFound();

        competition.Id = oldCompetition.Id;

        return Ok(await _repository.UpdateAsync(competition));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        var competition = await _repository.FindByIdAsync(id);

        if (competition == null)
            return NotFound();

        await _repository.DeleteAsync(competition);

        return Ok();
    }
}