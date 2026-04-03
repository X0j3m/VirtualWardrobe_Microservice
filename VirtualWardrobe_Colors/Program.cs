using Microsoft.EntityFrameworkCore;
using VirtualWardrobe_Colors.Data;
using VirtualWardrobe_Colors.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Przekaż pobraną zmienną (a nie nazwę klucza) do UseSqlServer
builder.Services.AddDbContext<ColorsDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped<ColorsService>();
builder.Services.AddScoped<ColorsRepository>();

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
