namespace PokemonReviewApp.WebAPI.Models;

public class Pokemon
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public DateTime BirthDate { get; set; }
    public ICollection<Review> Reviews { get; set; }
    public ICollection<PokemonOwner> PokemonOwners { get; set; }
    public ICollection<PokemonCategory> PokemonCategories { get; set; }
}