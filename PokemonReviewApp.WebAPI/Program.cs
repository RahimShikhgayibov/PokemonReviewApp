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
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// 2) Seeder
builder.Services.AddTransient<Seed>();

//  3) AutoMapper — manual registration
//    a) discover your Profile classes in this assembly (and any referenced ones)
 var mappingConfig = new MapperConfiguration(cfg =>
 {
     cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies());
 });
//    b) build the mapper
 IMapper mapper = mappingConfig.CreateMapper();
//    c) register both config and mapper
 builder.Services.AddSingleton(mappingConfig);
 builder.Services.AddSingleton(mapper);

// 4) Controllers & DI
builder.Services.AddControllers();
builder.Services.AddScoped<IPokemonRepository, PokemonRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICountryRepository, CountryRepository>();
builder.Services.AddScoped<IOwnerRepository, OwnerRepository>();
builder.Services.AddScoped<IReviewRepository, ReviewRepository>();
builder.Services.AddScoped<IReviewerRepository, ReviewerRepository>();

// 5) Swagger/OpenAPI
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

// auto‑seed in Development
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
    seeder.SeedDataContext();
}

// enable Swagger UI at root
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
    c.RoutePrefix = string.Empty;
});

app.UseAuthorization();
app.MapControllers();

app.Run();