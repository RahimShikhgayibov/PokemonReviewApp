using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI;
using PokemonReviewApp.WebAPI.Models;
using PokemonReviewApp.WebAPI.Repositories;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<Seed>();

builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Pokemon API",
        Version = "v1",
        Description = "API with automatic seeding"
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
    seeder.SeedDataContext();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();

//My own minimal test endpoint
app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();
