using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class CountryRepository : ICountryRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;


    public CountryRepository(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<CountryDto> GetCountries()
    {
        return _context.Countries.OrderBy(c => c.Id).ProjectTo<CountryDto>(_mapper.ConfigurationProvider).ToList();
    }

    public CountryDto GetCountry(int id)
    {
        return _context.Countries.Where(c => c.Id == id).ProjectTo<CountryDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }

    public CountryDto GetCountryByOwner(int ownerId)
    {
        return _context.Owners.Where(o => o.Id == ownerId).Select(c => c.Country).ProjectTo<CountryDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }

    public ICollection<OwnerDto> GetOwnersFromACountry(int countryId)
    {
        return _context.Owners.Where(o => o.Id == countryId).ProjectTo<OwnerDto>(_mapper.ConfigurationProvider).ToList();
    }

    public bool CountryExists(int id)
    {
        return _context.Countries.Any(e => e.Id == id);
    }
}