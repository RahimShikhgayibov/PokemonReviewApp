using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface ICategoryRepository
{
    ICollection<CategoryDto> GetCategories();
    CategoryDto GetCategory(int id);
    ICollection<PokemonDto> GetPokemonsByCategory(int id);
    bool CategoryExists(int id);
}