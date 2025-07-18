using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class PokemonRepository : IPokemonRepository
{
    private readonly IMapper _mapper ;
    private readonly AppDbContext _context;
    
    public PokemonRepository(AppDbContext dataContext,IMapper mapper)
    {
        _mapper = mapper;
        _context = dataContext;
    }


    public ICollection<PokemonDto> GetPokemons()
    {
        return _context.Pokemons
            .OrderBy(p => p.Id)
            .ProjectTo<PokemonDto>(_mapper.ConfigurationProvider)
            .ToList();
        
    }

    public PokemonDto GetPokemon(int id)
    {
        var dto = _context.Pokemons
                      .Where(p => p.Id == id)
                      .ProjectTo<PokemonDto>(_mapper.ConfigurationProvider)
                      .FirstOrDefault()
                  ?? throw new KeyNotFoundException($"No Pokemon found with Id={id}");

        return dto;
    }

    public PokemonDto CreatePokemon(PokemonDto pokemon)
    {
        if (!PokemonExists(pokemon.Id))
        {
            var entity = _mapper.Map<Pokemon>(pokemon);
            _context.Pokemons.Add(entity);
            _context.SaveChanges();
        
            return _mapper.Map<PokemonDto>(entity);
        }
        else
        {
            throw new Exception($"Pokemon with id {pokemon.Id} already exists");
        }
    }

    public PokemonDto UpdatePokemon(PokemonDto pokemon)
    {
        if (PokemonExists(pokemon.Id))
        {
            var entity = _mapper.Map<Pokemon>(pokemon);
            _context.Pokemons.Update(entity);
            _context.SaveChanges();
        
            return _mapper.Map<PokemonDto>(entity);
        }
        else
        {
            throw new Exception($"Pokemon with id {pokemon.Id} doesn't exists");
        }
    }

    public bool PokemonExists(int id)
    {
        return _context.Pokemons.Any(e => e.Id == id);
    }
}