namespace PokemonReviewApp.WebAPI.Models;

public class PokemonCategory
{
    public int PokemonId { get; set; }
    public Pokemon Pokemon { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}