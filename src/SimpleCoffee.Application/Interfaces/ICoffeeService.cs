using SimpleCoffee.Domain.Entities;

namespace SimpleCoffee.Application.Interfaces;

public interface ICoffeeService
{
    Task<IEnumerable<Coffee>> GetAllAsync();
    Task<Coffee?> GetByIdAsync(int id);
    Task<IEnumerable<Coffee>> GetPopularAsync();
    Task<IEnumerable<Coffee>> GetAvailableAsync();
}
