using AutoMapper;
using AutoMapper.QueryableExtensions;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Dtos;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

namespace PokemonReviewApp.WebAPI.Repositories;

public class ReviewerRepository : IReviewerRepository
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public ReviewerRepository(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public ICollection<ReviewerDto> GetReviewers()
    {
        return _context.Reviewers.ProjectTo<ReviewerDto>(_mapper.ConfigurationProvider).ToList();
    }

    public ReviewerDto GetReviewer(int id)
    {
        return _context.Reviewers.Where(r => r.Id == id).ProjectTo<ReviewerDto>(_mapper.ConfigurationProvider).FirstOrDefault();
    }

    public ICollection<ReviewDto> GetReviewsByReviewer(int reviewerId)
    {
        return _context.Reviews.Where(r => r.Id == reviewerId).ProjectTo<ReviewDto>(_mapper.ConfigurationProvider).ToList();
    }

    public bool ReviewerExists(int reviewerId)
    {
        return _context.Reviewers.Any(r => r.Id == reviewerId);
    }

    public ReviewerDto CreateReviewer(ReviewerDto reviewer)
    {
        if (!ReviewerExists(reviewer.Id))
        {
            var entity = _mapper.Map<Reviewer>(reviewer);
            _context.Reviewers.Add(entity);
            _context.SaveChanges();
        
            return _mapper.Map<ReviewerDto>(entity);
        }
        else
        {
            throw new Exception($"Reviewer with id {reviewer.Id} already exists");
        }
    }

    public ReviewerDto UpdateReviewer(ReviewerDto reviewer)
    {
        if (ReviewerExists(reviewer.Id))
        {
            var entity = _mapper.Map<Reviewer>(reviewer);
            _context.Reviewers.Update(entity);
            _context.SaveChanges();
        
            return _mapper.Map<ReviewerDto>(entity);
        }
        else
        {
            throw new Exception($"Reviewer with id {reviewer.Id} doesn't exists");
        }
    }
}