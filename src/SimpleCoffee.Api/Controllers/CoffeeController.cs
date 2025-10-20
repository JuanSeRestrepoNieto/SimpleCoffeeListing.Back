using Microsoft.AspNetCore.Mvc;
using SimpleCoffee.Application.Interfaces;
using SimpleCoffee.Domain.Entities;

namespace SimpleCoffee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoffeeController : ControllerBase
{
    private readonly ICoffeeService _coffeeService;

    public CoffeeController(ICoffeeService coffeeService)
    {
        _coffeeService = coffeeService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Coffee>>> GetAll()
    {
        var coffees = await _coffeeService.GetAllAsync();
        return Ok(coffees);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Coffee>> GetById(int id)
    {
        var coffee = await _coffeeService.GetByIdAsync(id);
        if (coffee == null)
        {
            return NotFound();
        }
        return Ok(coffee);
    }

    [HttpGet("popular")]
    public async Task<ActionResult<IEnumerable<Coffee>>> GetPopular()
    {
        var coffees = await _coffeeService.GetPopularAsync();
        return Ok(coffees);
    }

    [HttpGet("available")]
    public async Task<ActionResult<IEnumerable<Coffee>>> GetAvailable()
    {
        var coffees = await _coffeeService.GetAvailableAsync();
        return Ok(coffees);
    }
}
