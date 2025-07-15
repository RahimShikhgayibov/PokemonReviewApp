using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI.Repositories;
using PokemonReviewApp.WebAPI.Repositories.IRepositories;
using AutoMapper;
using PokemonReviewApp.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// 1) Database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(opts =>
    opts.UseNpgsql(connectionString));

// 2) Seeder
builder.Services.AddTransient<Seed>();

// 3) AutoMapper
var mappingConfig = new MapperConfiguration(cfg =>
{
    cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
});
IMapper mapper = mappingConfig.CreateMapper();
builder.Services.AddSingleton(mappingConfig);
builder.Services.AddSingleton(mapper);

// 4) Repositories & Controllers
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();

builder.Services.AddControllers();

// 5) CORS (optional—but helps if swagger UI can’t reach swagger.json)
builder.Services.AddCors(opts =>
    opts.AddDefaultPolicy(policy =>
        policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

// 6) Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {
        Title = "Pokemon API", Version = "v1",
        Description = "API with automatic seeding"
    });
});

var app = builder.Build();

// Auto‑seed if in Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    scope.ServiceProvider.GetRequiredService<Seed>().SeedDataContext();
}

// 7) Middleware pipeline
//app.UseHttpsRedirection();

app.UseRouting();

// Apply CORS (if you enabled it)
app.UseCors();

app.UseAuthorization();

// Serve swagger JSON and UI at root
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
    c.RoutePrefix = string.Empty;    // UI at "/"
});

// Map your controllers
app.MapControllers();

app.Run();
