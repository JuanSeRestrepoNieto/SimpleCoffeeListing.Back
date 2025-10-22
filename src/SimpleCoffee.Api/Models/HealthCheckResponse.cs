namespace SimpleCoffee.Api.Models;

// Respuesta del endpoint de health con configuración no sensible.
public sealed class HealthCheckResponse
{
  public string Status { get; init; } = "Healthy";
  public string Environment { get; init; } = "Production";
  public string AppName { get; init; } = "SimpleCoffee API";
  public string Version { get; init; } = "unknown";
  public DateTime Utc { get; init; } = DateTime.UtcNow;

  // Configuración no sensible a exponer
  public object Config { get; init; } = new { };
}