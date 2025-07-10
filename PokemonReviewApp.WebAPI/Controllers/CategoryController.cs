using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController : ControllerBase
{
    private readonly ICategoryRepository _categoryRepository;

    public CategoryController(ICategoryRepository categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Category>> GetCategories()
    {
        var categories = _categoryRepository.GetCategories();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public ActionResult<Category> GetCategory(int id)
    {
        var category = _categoryRepository.GetCategory(id);
        return Ok(category);
    }

    [HttpGet("categories/{categoryId}")]
    public ActionResult<IEnumerable<Pokemon>> GetCategoryByCategoryId(int categoryId)
    {
        var pokemons = _categoryRepository.GetPokemonsByCategory(categoryId);
        return Ok(pokemons);
    }
}