using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class OwnerRepository : IOwnerRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public OwnerRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<OwnerDto> GetOwners()
    {
        return _context.Owners.OrderBy(o => o.Id).ProjectTo<OwnerDto>(_mapper.ConfigurationProvider).ToList();
    }

    public OwnerDto GetOwner(int id)
    {
        return _context.Owners.Where(o => o.Id == id).ProjectTo<OwnerDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }

    public ICollection<OwnerDto> GetOwnerOfPokemon(int pokemonId)
    {
        return _context.PokemonOwners.Where(o => o.PokemonId == pokemonId).Select(a => a.Owner).ProjectTo<OwnerDto>(_mapper.ConfigurationProvider).ToList();
    }

    public ICollection<PokemonDto> GetPokemonByOwner(int ownerId)
    {
        return _context.PokemonOwners.Where(o => o.OwnerId == ownerId).Select(a => a.Pokemon).ProjectTo<PokemonDto>(_mapper.ConfigurationProvider).ToList();
    }

    public bool OwnerExists(int id)
    {
        return _context.Owners.Any(o => o.Id == id);
    }

    public OwnerDto CreateOwner(OwnerDto ownerDto)
    {
        if (!OwnerExists(ownerDto.Id))
        {
            var entity = _mapper.Map<Owner>(ownerDto);
            _context.Owners.Add(entity);
            _context.SaveChanges();
        
            return _mapper.Map<OwnerDto>(entity);
        }
        else
        {
            throw new Exception($"Owner with id {ownerDto.Id} already exists");
        }
    }
}