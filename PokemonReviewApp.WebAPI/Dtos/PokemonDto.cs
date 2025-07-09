namespace PokemonReviewApp.WebAPI.Dtos;

public class PokemonDto
{
    public required int Id { get; set; }
    public required string Name { get; set; }
    public DateTime BirthDate { get; set; }
}