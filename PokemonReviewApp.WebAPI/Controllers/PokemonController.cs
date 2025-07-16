using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/pokemons")]
public class PokemonController : ControllerBase
{
    private readonly IPokemonRepository _pokemonRepository;

    public PokemonController(IPokemonRepository pokemonRepository)
    {
        _pokemonRepository = pokemonRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<PokemonDto>> GetPokemons()
    {
        var pokemons = _pokemonRepository.GetPokemons();
        return Ok(pokemons);
    }

    [HttpGet("{id}")]
    public ActionResult<PokemonDto> GetPokemon(int id)
    {
        var pokemon = _pokemonRepository.GetPokemon(id);
        return Ok(pokemon);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<PokemonDto> CreatePokemon([FromBody] PokemonDto pokemonDto)
    {
        if (pokemonDto == null || string.IsNullOrWhiteSpace(pokemonDto.Name))
            return BadRequest();

        var created = _pokemonRepository.CreatePokemon(pokemonDto);
        
        return CreatedAtAction(
            nameof(GetPokemon),
            new { id = created.Id },
            created);
    }
}