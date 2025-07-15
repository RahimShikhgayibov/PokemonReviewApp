using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public CategoryRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<CategoryDto> GetCategories()
    {
        return _context.Categories.OrderBy(a => a.Id).ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).ToList();
    }

    public CategoryDto GetCategory(int id)
    {
        return _context.Categories.ProjectTo<CategoryDto>(_mapper.ConfigurationProvider).FirstOrDefault(a => a.Id == id)?? throw new KeyNotFoundException($"No Category found with Id={id}");
    }

    public ICollection<PokemonDto> GetPokemonsByCategory(int id)
    {
        return _context.PokemonCategories
            .Where(pc => pc.CategoryId == id)
            .Select(pc => pc.Pokemon)
            .ProjectTo<PokemonDto>(_mapper.ConfigurationProvider)
            .ToList();
    }


    public bool CategoryExists(int id)
    {
        return _context.Categories.Any(e => e.Id == id);
    }

    public CategoryDto CreateCategory(CategoryDto categoryDto)
    {
        if (!CategoryExists(categoryDto.Id))
        {
            var entity = _mapper.Map<Category>(categoryDto);
            _context.Categories.Add(entity);
            _context.SaveChanges();
        
            return _mapper.Map<CategoryDto>(entity);
        }
        else
        {
            throw new Exception($"Category with id {categoryDto.Id} already exists");
        }
    }

    
}