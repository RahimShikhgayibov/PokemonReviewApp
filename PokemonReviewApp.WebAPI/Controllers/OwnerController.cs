using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OwnerController : ControllerBase
{
    private readonly IOwnerRepository _ownerRepository;

    public OwnerController(IOwnerRepository ownerRepository)
    {
        _ownerRepository = ownerRepository;
    }
 
    [HttpGet]
    public ActionResult<IEnumerable<Owner>> GetOwners()
    {
        var owners = _ownerRepository.GetOwners();
        return Ok(owners);
    }
    
    [HttpGet("owners/{id}")]
    public ActionResult<Owner> GetOwner(int id)
    {
        var owner = _ownerRepository.GetOwner(id);
        return Ok(owner);
    }
    
    [HttpGet("owners/pokemon/{pokemonId}")]
    public ActionResult<IEnumerable<Owner>> GetOwnerOfPokemon(int pokemonId)
    {
        var owners = _ownerRepository.GetOwnerOfPokemon(pokemonId);
        return Ok(owners);
    }
    
    [HttpGet("owners/owner/{pokemonId}")]
    public ActionResult<IEnumerable<Pokemon>> GetPokemonByOwner(int ownerId)
    {
        var pokemons = _ownerRepository.GetPokemonByOwner(ownerId);
        return Ok(pokemons);
    }
    
    
}