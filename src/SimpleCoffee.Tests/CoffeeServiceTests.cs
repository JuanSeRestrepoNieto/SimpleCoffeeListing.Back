using SimpleCoffee.Application.Interfaces;
using SimpleCoffee.Application.Services;
using SimpleCoffee.Domain.Entities;
using Moq;

namespace SimpleCoffee.Tests;

public class CoffeeServiceTests
{
    private readonly Mock<ICoffeeRepository> _mockRepository;
    private readonly CoffeeService _service;

    public CoffeeServiceTests()
    {
        _mockRepository = new Mock<ICoffeeRepository>();
        _service = new CoffeeService(_mockRepository.Object);
    }

    [Fact]
    public async Task GetAllAsync_ReturnsAllCoffees()
    {
        // Arrange
        var coffees = new List<Coffee>
        {
            new Coffee { Id = 1, Name = "Cappuccino", Popular = true, Available = true },
            new Coffee { Id = 2, Name = "Espresso", Popular = false, Available = true }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(coffees);

        // Act
        var result = await _service.GetAllAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsCorrectCoffee()
    {
        // Arrange
        var coffee = new Coffee { Id = 1, Name = "Cappuccino", Popular = true, Available = true };
        _mockRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(coffee);

        // Act
        var result = await _service.GetByIdAsync(1);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Cappuccino", result.Name);
    }

    [Fact]
    public async Task GetPopularAsync_ReturnsOnlyPopularCoffees()
    {
        // Arrange
        var coffees = new List<Coffee>
        {
            new Coffee { Id = 1, Name = "Cappuccino", Popular = true, Available = true },
            new Coffee { Id = 2, Name = "Espresso", Popular = false, Available = true },
            new Coffee { Id = 3, Name = "Latte", Popular = true, Available = true }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(coffees);

        // Act
        var result = await _service.GetPopularAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, c => Assert.True(c.Popular));
    }

    [Fact]
    public async Task GetAvailableAsync_ReturnsOnlyAvailableCoffees()
    {
        // Arrange
        var coffees = new List<Coffee>
        {
            new Coffee { Id = 1, Name = "Cappuccino", Popular = true, Available = true },
            new Coffee { Id = 2, Name = "Espresso", Popular = false, Available = false },
            new Coffee { Id = 3, Name = "Latte", Popular = true, Available = true }
        };
        _mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(coffees);

        // Act
        var result = await _service.GetAvailableAsync();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Count());
        Assert.All(result, c => Assert.True(c.Available));
    }
}
