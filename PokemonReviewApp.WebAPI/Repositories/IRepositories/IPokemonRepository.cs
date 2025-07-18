using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface IPokemonRepository
{
    ICollection<PokemonDto> GetPokemons();
    PokemonDto GetPokemon(int id);
    PokemonDto CreatePokemon(PokemonDto pokemon);
    PokemonDto UpdatePokemon(PokemonDto pokemon);
    bool PokemonExists(int id);
}