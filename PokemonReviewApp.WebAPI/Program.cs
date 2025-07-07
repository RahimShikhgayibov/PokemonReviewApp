using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Test API",
        Version = "v1",
        Description = "A minimal API for testing Swagger"
    });
});

var app = builder.Build();

// Enable Swagger middleware
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Test API V1");
    c.RoutePrefix = string.Empty; // Swagger UI served at "/"
});

// Dummy endpoint
app.MapGet("/ping", () => Results.Ok("pong"));

app.Run();