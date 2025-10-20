using SimpleCoffee.Domain.Entities;

namespace SimpleCoffee.Application.Interfaces;

public interface ICoffeeRepository
{
    Task<IEnumerable<Coffee>> GetAllAsync();
    Task<Coffee?> GetByIdAsync(int id);
}
