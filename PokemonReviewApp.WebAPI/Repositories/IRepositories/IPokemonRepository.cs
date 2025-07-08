using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface IPokemonRepository
{
    ICollection<Pokemon> GetPokemons();
}