using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface IReviewerRepository
{
    ICollection<ReviewerDto> GetReviewers();
    ReviewerDto GetReviewer(int id);
    ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId);
    bool reviewerExists(int reviewerId);
}