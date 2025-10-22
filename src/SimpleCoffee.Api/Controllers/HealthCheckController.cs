using Microsoft.AspNetCore.Mvc;
using SimpleCoffee.Api.Models;

namespace SimpleCoffee.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class HealthController : ControllerBase
{
  private readonly IConfiguration _configuration;
  private readonly IHostEnvironment _environment;

  public HealthController(IConfiguration configuration, IHostEnvironment environment)
  {
    _configuration = configuration;
    _environment = environment;
  }

  // GET /api/health
  [HttpGet]
  public IActionResult Get()
  {
    // Solo exponer claves no sensibles
    var allowedHosts = _configuration["AllowedHosts"];
    var loggingLevels = _configuration
        .GetSection("Logging:LogLevel")
        .GetChildren()
        .ToDictionary(c => c.Key, c => c.Value);

    var response = new HealthCheckResponse
    {
      Status = "Healthy",
      Environment = _environment.EnvironmentName,
      AppName = _configuration["AppInfo:Name"] ?? "SimpleCoffee API",
      Version = _configuration["AppInfo:Version"] ?? "unknown",
      Utc = DateTime.UtcNow,
      Config = new
      {
        AllowedHosts = allowedHosts,
        Logging = loggingLevels
      }
    };

    return Ok(response);
  }
}