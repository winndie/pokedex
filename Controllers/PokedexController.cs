using Microsoft.AspNetCore.Mvc;
using Pokedex.Models;
using Pokedex.Services;

namespace Pokedex.Controllers;

[ApiController]
[Route("[controller]")]
public class PokedexController : ControllerBase
{
    private readonly ILogger<PokedexController> _logger;
    private readonly IPokedexService _pokedexService;

    public PokedexController(
        IPokedexService pokedexService,
        ILogger<PokedexController> logger)
    {
        _pokedexService = pokedexService;
        _logger = logger;
    }

    [HttpGet("GetEvolutionChainByPokemonName/{pokemonName}")]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Variation))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Get(string pokemonName)
    {
        ServiceResult<Variation> evolutions = _pokedexService.GetEvolutionChain(pokemonName);
        return evolutions.IsSuccess ? Ok(evolutions.Data) : NotFound();
    }
}

