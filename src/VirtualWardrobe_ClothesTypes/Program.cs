using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_ClothesTypes.Data;
using VirtualWardrobe_ClothesTypes.Interface;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ClothesTypeDbContext>(
    options =>
    {
        options.UseSqlServer(connectionString);
    }
    );

builder.Services.AddScoped<IClothesTypeRepository, ClothesTypeRepository>();
builder.Services.AddScoped<IClothesLayerRepository, ClothesLayerRepository>();

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
