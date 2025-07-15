using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Dtos;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface ICountryRepository
{
    ICollection<CountryDto> GetCountries();
    CountryDto GetCountry(int id);
    CountryDto GetCountryByOwner(int ownerId);
    ICollection<OwnerDto> GetOwnersFromACountry(int countryId);
    bool CountryExists(int id);
    CountryDto CreateCountry(CountryDto country);
}