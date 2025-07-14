using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class ReviewRepository : IReviewRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ReviewRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<ReviewDto> GetReviews()
    {
        return _context.Reviews.ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToList();
    }

    public ICollection<ReviewDto> GetReviewsOfPokemon(int pokemonId)
    {
        return _context.Reviews.Where(r => r.PokemonId == pokemonId).ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToList();
    }

    public ReviewDto GetReview(int id)
    {
        return _context.Reviews.Where(r => r.Id == id).ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }

    public bool ReviewExists(int id)
    {
        return _context.Reviews.Any(r => r.Id == id);
    }
}