using System.Text.Json;
using SimpleCoffee.Domain.Entities;
using SimpleCoffee.Infrastructure.Repositories;

namespace SimpleCoffee.Tests;

public class CoffeeRepositoryTests
{
    [Fact]
    public async Task GetAllAsync_LoadsDataFromJson()
    {
        // Arrange
        var repository = new CoffeeRepository();

        // Act
        var result = await repository.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.NotEmpty(result);
        Assert.Equal(6, result.Count());
    }

    [Fact]
    public async Task GetAllAsync_DeserializesCorrectly()
    {
        // Arrange
        var repository = new CoffeeRepository();

        // Act
        var result = await repository.GetAllAsync();
        var cappuccino = result.First();

        // Assert
        Assert.Equal(1, cappuccino.Id);
        Assert.Equal("Cappuccino", cappuccino.Name);
        Assert.Equal("$5.20", cappuccino.Price);
        Assert.Equal(4.7, cappuccino.Rating);
        Assert.Equal(65, cappuccino.Votes);
        Assert.True(cappuccino.Popular);
        Assert.True(cappuccino.Available);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectCoffee()
    {
        // Arrange
        var repository = new CoffeeRepository();

        // Act
        var result = await repository.GetByIdAsync(2);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("House Coffee", result.Name);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        // Arrange
        var repository = new CoffeeRepository();

        // Act
        var result = await repository.GetByIdAsync(999);

        // Assert
        Assert.Null(result);
    }

    [Fact]
    public async Task GetAllAsync_HandlesNullRating()
    {
        // Arrange
        var repository = new CoffeeRepository();

        // Act
        var result = await repository.GetAllAsync();
        var valentineSpecial = result.FirstOrDefault(c => c.Id == 6);

        // Assert
        Assert.NotNull(valentineSpecial);
        Assert.Equal("Valentine Special", valentineSpecial.Name);
        Assert.Null(valentineSpecial.Rating);
        Assert.Equal(0, valentineSpecial.Votes);
    }
}
