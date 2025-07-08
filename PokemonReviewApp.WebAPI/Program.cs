// using Microsoft.EntityFrameworkCore;
// using Microsoft.OpenApi.Models;
// using PokemonReviewApp.WebAPI;
// using PokemonReviewApp.WebAPI.Data;
//
// var builder = WebApplication.CreateBuilder(args);
//
// builder.Services.AddControllers();
// builder.Services.AddTransient<Seed>();
//
// var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
// builder.Services.AddDbContext<AppDbContext>(options =>
//     options.UseNpgsql(connectionString));
//
// // Register Swagger services
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", new OpenApiInfo
//     {
//         Title = "Test API",
//         Version = "v1",
//         Description = "A minimal API for testing Swagger"
//     });
// });
//
// var app = builder.Build();
//
// if (args.Length == 1 && args[0].ToLower() == "seeddata")
//     SeedData(app);
//
// void SeedData(IHost app)
// {
//     var scopedFactory = app.Services.GetService<IServiceScopeFactory>();
//
//     using (var scope = scopedFactory.CreateScope())
//     {
//         var service = scope.ServiceProvider.GetService<Seed>();
//         service.SeedDataContext();
//     }
// }
//
// // Enable Swagger middleware
// app.UseSwagger();
// app.UseSwaggerUI(c =>
// {
//     c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
//     c.RoutePrefix = string.Empty; // Swagger UI served at "/"
// });
//
// // Dummy endpoint
// app.MapGet("/ping", () => Results.Ok("pong"));
//
// app.Run();


using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PokemonReviewApp.WebAPI.Data;
using PokemonReviewApp.WebAPI;

// build the WebApplication
var builder = WebApplication.CreateBuilder(args);

// register your DbContext
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// register the seeder
builder.Services.AddTransient<Seed>();

// add controllers/endpoints and Swagger
builder.Services.AddControllers();
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

// automatically seed when in Development and if not already seeded
if (app.Environment.IsDevelopment())
{
    using var scope = app.Services.CreateScope();
    var seeder = scope.ServiceProvider.GetRequiredService<Seed>();
    seeder.SeedDataContext();
}

// enable middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Pokemon API V1");
    c.RoutePrefix = string.Empty;
});

//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// you can still keep minimal endpoints:
app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();
