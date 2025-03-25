namespace schiessmeister_csharp.Api.Controller;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

[Authorize]  // Authentifizierung für alle Routen notwendig
[ApiController]
[Route("api/shooter")] // Basisroute für alle Enpunkte
public class ShooterController : ControllerBase {
    private readonly IShooterRepository _repository;

    //Konstruktor
    public ShooterController(IShooterRepository repository) {
        _repository = repository;
    }
    //GET: api/shooter - Ruft alle Shooter aus der Datenbank ab
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [AllowAnonymous]  // Alle dürfen Shooter-Liste abrufen
    public async Task<ActionResult<IEnumerable<Shooter>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }

    // GET: api/shooter/{id} - Ruft einen spezifischen Shooter ab
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> GetById(int id) {
        var shooter = await _repository.FindByIdAsync(id);
        if (shooter == null) {
            return NotFound();
        }
        return Ok(shooter);
    }

    // POST: api/shooter - Erstellt einen neuen Shooter
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<Shooter>> Create(Shooter shooter) {
        if (shooter == null || string.IsNullOrEmpty(shooter.Name)) {
            return BadRequest("Invalid shooter data"); // Falls keine Daten übermittelt wurden
        }
        await _repository.AddAsync(shooter); // Fügt den neuen Shooter zur Datenbank hinzu
        return CreatedAtAction(nameof(GetById), new { id = shooter.Id }, shooter); // Gibt den erstellten Shooter zurück
    }

    // PUT: api/shooter/{id} - Aktualisiert einen bestehenden Shooter
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(int id, Shooter shooter) {
        if (id != shooter.Id) {
            return BadRequest();
        }
        var updated = await _repository.UpdateAsync(shooter);
        if (updated == null) {
            return NotFound();
        }
        return Ok();
    }

    [HttpDelete]
    [Route("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<Shooter>> DeleteShooter(int id) {
        var existingShooter = await _repository.FindByIdAsync(id);
        if (existingShooter == null)
            return NotFound();

        await _repository.DeleteAsync(existingShooter);

        return Ok();
    }

}