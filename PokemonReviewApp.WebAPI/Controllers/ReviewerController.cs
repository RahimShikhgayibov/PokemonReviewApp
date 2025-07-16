using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewerController : ControllerBase
{
    private readonly IReviewerRepository _reviewerRepository;

    public ReviewerController(IReviewerRepository reviewerRepository)
    {
        _reviewerRepository = reviewerRepository;
    }
    
    [HttpGet]
    public ActionResult<IEnumerable<ReviewerDto>> GetReviewers()
    {
        var reviewers = _reviewerRepository.GetReviewers();
        return Ok(reviewers);
    }

    [HttpGet("{id}")]
    public ActionResult<ReviewerDto> GetReviewer(int id)
    {
       var reviewer = _reviewerRepository.GetReviewer(id);
       return Ok(reviewer);
    }

    [HttpGet("Review/{reviewerId}")]
    public ActionResult<IEnumerable<ReviewDto>> GetReviewsByReviewer(int reviewerId)
    {
        var reviews = _reviewerRepository.GetReviewsByReviewer(reviewerId);
        return Ok(reviews);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<ReviewerDto> CreateCategory([FromBody] ReviewerDto reviewerDto)
    {
        if (reviewerDto == null || string.IsNullOrWhiteSpace(reviewerDto.FirstName))
            return BadRequest();

        var created = _reviewerRepository.CreateReviewer(reviewerDto);
        
        return CreatedAtAction(
            nameof(GetReviewer),
            new { id = created.Id },
            created);
    }
}