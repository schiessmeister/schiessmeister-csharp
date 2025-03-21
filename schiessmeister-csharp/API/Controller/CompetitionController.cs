namespace schiessmeister_csharp.Api.Controller;

using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

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
}