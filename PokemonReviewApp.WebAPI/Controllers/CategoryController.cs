using Microsoft.AspNetCore.Mvc;
using PokemonReviewApp.WebAPI.Dtos;
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

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryDto> CreateCategory([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            return BadRequest();

        var created = _categoryRepository.CreateCategory(categoryDto);
        
        return CreatedAtAction(
            nameof(GetCategory),
            new { id = created.Id },
            created);
    }
    
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<CategoryDto> UpdateCategory([FromBody] CategoryDto categoryDto)
    {
        if (categoryDto == null || string.IsNullOrWhiteSpace(categoryDto.Name))
            return BadRequest();

        var created = _categoryRepository.UpdateCategory(categoryDto);
        
        return CreatedAtAction(
            nameof(GetCategory),
            new { id = created.Id },
            created);
    }
    
}