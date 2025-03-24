using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.Api.Controller;

[ApiController]
[Route("api/competition")]
public class CompetitionController : ControllerBase {
    private readonly ICompetitionRepository _repository;

    public CompetitionController(ICompetitionRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Competition>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Competition>> Post(Competition comp) {
        var newComp = await _repository.AddAsync(comp);

        return CreatedAtAction(nameof(Get), new { id = newComp.Id }, newComp);
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
}