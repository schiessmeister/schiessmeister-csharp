using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/shooter")]
public class ShooterController : ControllerBase {
    private readonly IShooterRepository _repository;

    public ShooterController(IShooterRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Shooter>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> Get(int id) {
        var shooter = await _repository.FindByIdAsync(id);

        if (shooter == null)
            return NotFound();

        return Ok(shooter);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Shooter>> Create([FromBody] Shooter shooter) {
        var newShooter = await _repository.AddAsync(shooter);

        return CreatedAtAction(nameof(Get), new { id = newShooter.Id }, newShooter);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> Update(int id, [FromBody] Shooter shooter) {
        var existingShooter = await _repository.FindByIdAsync(id);

        if (existingShooter == null)
            return NotFound();

        shooter.Id = id;

        return Ok(await _repository.UpdateAsync(shooter));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        var shooter = await _repository.FindByIdAsync(id);

        if (shooter == null)
            return NotFound();

        await _repository.DeleteAsync(shooter);

        return Ok();
    }
}