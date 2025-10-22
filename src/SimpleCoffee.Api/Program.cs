using SimpleCoffee.Application.Interfaces;
using SimpleCoffee.Application.Services;
using SimpleCoffee.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register application services
builder.Services.AddSingleton<ICoffeeRepository, CoffeeRepository>();
builder.Services.AddScoped<ICoffeeService, CoffeeService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapControllers();

app.Run();