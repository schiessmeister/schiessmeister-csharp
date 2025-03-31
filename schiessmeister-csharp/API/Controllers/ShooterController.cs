using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

namespace schiessmeister_csharp.API.Controllers;

[ApiController]
[Route("api/shooter")]
public class ShooterController : ControllerBase {
    private readonly IShooterRepository _shooters;

    public ShooterController(IShooterRepository shooters) {
        _shooters = shooters;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Shooter>>> GetAll() {
        return Ok(await _shooters.FindAllAsync());
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> Get(int id) {
        var shooter = await _shooters.FindByIdAsync(id);
        if (shooter == null)
            return NotFound();

        return Ok(shooter);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Shooter>> Create(Shooter shooter) {
        var newShooter = await _shooters.AddAsync(shooter);

        return CreatedAtAction(nameof(Get), new { id = newShooter.Id }, newShooter);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> Update(int id, Shooter shooter) {
        shooter.Id = id;

        return Ok(await _shooters.UpdateAsync(shooter));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id) {
        var shooter = await _shooters.FindByIdAsync(id);
        if (shooter == null)
            return NotFound();

        await _shooters.DeleteAsync(shooter);

        return Ok();
    }
}