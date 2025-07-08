namespace PokemonReviewApp.WebAPI.Models;

public class Country
{
    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Owner> Owners { get; set; }
}