using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.WebAPI.Models;

namespace PokemonReviewApp.WebAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Pokemon> Pokemons { get; set; }
    public DbSet<PokemonCategory> PokemonCategories { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<PokemonOwner> PokemonOwners { get; set; }
    public DbSet<Owner> Owners { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Review> Reviews { get; set; }
    public DbSet<Reviewer> Reviewers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<PokemonCategory>()
            .HasKey(pc => new { pc.CategoryId, pc.PokemonId });

        builder.Entity<PokemonCategory>()
            .HasOne(p => p.Pokemon)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(p => p.PokemonId);
        
        builder.Entity<PokemonCategory>()
            .HasOne(p => p.Category)
            .WithMany(pc => pc.PokemonCategories)
            .HasForeignKey(p => p.CategoryId);
        
        builder.Entity<PokemonOwner>()
            .HasKey(po => new { po.OwnerId, po.PokemonId });

        builder.Entity<PokemonOwner>()
            .HasOne(p => p.Pokemon)
            .WithMany(po => po.PokemonOwners)
            .HasForeignKey(p => p.PokemonId);
        
        builder.Entity<PokemonOwner>()
            .HasOne(p => p.Owner)
            .WithMany(po => po.PokemonOwners)
            .HasForeignKey(p => p.OwnerId);
    }
    
}
