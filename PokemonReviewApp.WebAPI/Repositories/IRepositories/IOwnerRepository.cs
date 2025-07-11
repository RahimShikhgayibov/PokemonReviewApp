using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface IOwnerRepository
{
    ICollection<OwnerDto> GetOwners();
    OwnerDto GetOwner(int id);
    ICollection<OwnerDto> GetOwnerOfPokemon(int pokemonId);
    ICollection<PokemonDto> GetPokemonByOwner(int ownerId);
    bool OwnerExists(int id);
}