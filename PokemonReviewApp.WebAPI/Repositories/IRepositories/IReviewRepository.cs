using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Repositories.IRepositories;

public interface IReviewRepository
{
    ICollection<ReviewDto> GetReviews();
    ICollection<ReviewDto> GetReviewsOfPokemon(int pokemonId);
    ReviewDto GetReview(int id);
    bool ReviewExists(int id);
    ReviewDto CreateReview(ReviewDto review);
    ReviewDto UpdateReview(ReviewDto review);
}