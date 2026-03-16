using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_ClothesTypes.Data;
using VirtualWardrobe_ClothesTypes.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ClothesTypeContext>(
    options =>
    {
        options.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=VirtualWardrobe_ClothesTypes;Trusted_Connection=True;TrustServerCertificate=True;");
    }
    );
builder.Services.AddScoped<ClothesTypeService>();
builder.Services.AddScoped<ClothesTypeRepository>();

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
