using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly AppDbContext _context;
    
    public PokemonRepository(AppDbContext dataContext)
    {
        _context = dataContext;
    }


    public ICollection<Pokemon> GetPokemons()
    {
        return _context.Pokemons.OrderBy(p => p.Id).ToList();
    }
}