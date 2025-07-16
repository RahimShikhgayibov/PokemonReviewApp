using AutoMapper;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Helpers;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Pokemon, PokemonDto>();
        CreateMap<PokemonDto, Pokemon>();
        CreateMap<Category, CategoryDto>();
        CreateMap<CategoryDto, Category>();
        CreateMap<Country, CountryDto>();
        CreateMap<CountryDto, Country>();
        CreateMap<Owner, OwnerDto>();
        CreateMap<OwnerDto, Owner>();
        CreateMap<Review, ReviewDto>();
        CreateMap<ReviewDto, Review>();
        CreateMap<Reviewer, ReviewerDto>();
        CreateMap<ReviewerDto, Reviewer>();
    }
}