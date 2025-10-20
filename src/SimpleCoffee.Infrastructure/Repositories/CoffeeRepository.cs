using System.Text.Json;
using SimpleCoffee.Application.Interfaces;
using SimpleCoffee.Domain.Entities;

namespace SimpleCoffee.Infrastructure.Repositories;

public class CoffeeRepository : ICoffeeRepository
{
  private readonly string _dataFilePath;
  private List<Coffee>? _cachedCoffees;
  private readonly SemaphoreSlim _semaphore = new(1, 1);

  public CoffeeRepository()
  {
    _dataFilePath = Path.Combine(AppContext.BaseDirectory, "Data", "coffee-data.json");
  }

  public async Task<IEnumerable<Coffee>> GetAllAsync()
  {
    if (_cachedCoffees == null)
    {
      await _semaphore.WaitAsync();
      try
      {
        if (_cachedCoffees == null)
          await LoadDataAsync();
      }
      finally
      {
        _semaphore.Release();
      }
    }
    return _cachedCoffees ?? new List<Coffee>();
  }

  public async Task<Coffee?> GetByIdAsync(int id)
  {
    var coffees = await GetAllAsync();
    return coffees.FirstOrDefault(c => c.Id == id);
  }

  private async Task LoadDataAsync()
  {
    if (!File.Exists(_dataFilePath))
    {
      throw new FileNotFoundException($"Coffee data file not found at: {_dataFilePath}");
    }

    var jsonContent = await File.ReadAllTextAsync(_dataFilePath);
    _cachedCoffees = JsonSerializer.Deserialize<List<Coffee>>(jsonContent, new JsonSerializerOptions
    {
      PropertyNameCaseInsensitive = true
    }) ?? new List<Coffee>();
  }
}
