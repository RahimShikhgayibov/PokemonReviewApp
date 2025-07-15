using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountryController : ControllerBase
{ 
    private readonly ICountryRepository _countryRepository;

    public CountryController(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<CountryDto>> GetCountries()
    {
        var countries = _countryRepository.GetCountries();
        return Ok(countries);
    }

    [HttpGet("{id}")]
    public ActionResult<CountryDto> GetCountry(int id)
    {
        var country = _countryRepository.GetCountry(id);
        return Ok(country);
    }

    [HttpGet("countries/owners/{ownerId}")]
    public ActionResult<IEnumerable<OwnerDto>> GetCountryByOwner(int ownerId)
    {
        var country = _countryRepository.GetCountryByOwner(ownerId);
        return Ok(country);
    }
    
    [HttpGet("countries/categories/{categoryId}")]
    public ActionResult<IEnumerable<OwnerDto>> GetOwnersFromACountry(int categoryId)
    {
        var country = _countryRepository.GetOwnersFromACountry(categoryId);
        return Ok(country);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CountryDto> CreateCategory([FromBody] CountryDto countryDto)
    {
        if (countryDto == null || string.IsNullOrWhiteSpace(countryDto.Name))
            return BadRequest();

        var created = _countryRepository.CreateCountry(countryDto);
        
        return CreatedAtAction(
            nameof(GetCountry),
            new { id = created.Id },
            created);
    }
}