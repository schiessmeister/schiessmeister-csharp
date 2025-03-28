﻿namespace schiessmeister_csharp.Api.Controller;

using Microsoft.AspNetCore.Mvc;
using schiessmeister_csharp.Domain.Models;
using schiessmeister_csharp.Domain.Repositories;

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
}