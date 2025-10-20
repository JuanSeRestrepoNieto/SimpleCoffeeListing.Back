using SimpleCoffee.Application.Interfaces;
using SimpleCoffee.Domain.Entities;

namespace SimpleCoffee.Application.Services;

public class CoffeeService : ICoffeeService
{
    private readonly ICoffeeRepository _repository;

    public CoffeeService(ICoffeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<Coffee>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Coffee?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Coffee>> GetPopularAsync()
    {
        var coffees = await _repository.GetAllAsync();
        return coffees.Where(c => c.Popular);
    }

    public async Task<IEnumerable<Coffee>> GetAvailableAsync()
    {
        var coffees = await _repository.GetAllAsync();
        return coffees.Where(c => c.Available);
    }
}
