using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewRepository _reviewRepository;

    public ReviewController(IReviewRepository reviewRepository)
    {
        _reviewRepository = reviewRepository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<ReviewDto>> GetReviews()
    {
        var reviews =  _reviewRepository.GetReviews();
        return Ok(reviews);
    }

    [HttpGet("pokemon/{pokemonId}")]
    public ActionResult<IEnumerable<ReviewDto>> GetReviewsOfPokemon(int pokemonId)
    {
        var reviews = _reviewRepository.GetReviewsOfPokemon(pokemonId);
        return Ok(reviews);
    }

    [HttpGet("{id}")]
    public ActionResult<ReviewDto> GetReview(int id)
    {
        var review = _reviewRepository.GetReview(id);
        return Ok(review);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReviewDto> CreateReview([FromBody] ReviewDto reviewDto)
    {
        if (reviewDto == null || string.IsNullOrWhiteSpace(reviewDto.Text))
            return BadRequest();

        var created = _reviewRepository.CreateReview(reviewDto);
        
        return CreatedAtAction(
            nameof(GetReview),
            new { id = created.Id },
            created);
    }
    
}