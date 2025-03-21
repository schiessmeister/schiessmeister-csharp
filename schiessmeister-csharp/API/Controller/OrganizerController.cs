namespace schiessmeister_csharp.Api.Controller;

using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

[ApiController]
[Route("api/organizer")]
public class OrganizerController : ControllerBase {
    private readonly IOrganizerRepository _repository;

    public OrganizerController(IOrganizerRepository repository) {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<Organizer>>> GetAll() {
        return Ok(await _repository.FindAllAsync());
    }
}