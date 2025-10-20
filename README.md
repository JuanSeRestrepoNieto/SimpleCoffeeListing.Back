# ☕ Simple Coffee Listing API (.NET 8 + Clean Architecture)

This project defines the structure and requirements for an **ASP.NET Core 8 Web API** built using **Clean Architecture principles**.  
The API should expose a static list of coffee products loaded from a local **JSON file** and provide endpoints to retrieve and filter the data.

---

## 🧱 Project Structure

The solution must follow a **Clean Architecture** layout:

src/
├── SimpleCoffee.Api # Presentation layer (Controllers, DTOs, Program.cs)
├── SimpleCoffee.Application # Application layer (Use Cases, Interfaces, Services)
├── SimpleCoffee.Domain # Core entities and domain logic
├── SimpleCoffee.Infrastructure # Infrastructure (Data access, repositories, JSON loading)
└── SimpleCoffee.Tests # Unit and integration tests

### Layer responsibilities

| Layer              | Description                                                               |
| ------------------ | ------------------------------------------------------------------------- |
| **Domain**         | Core entities and business rules, no external dependencies.               |
| **Application**    | Use cases and interfaces for domain operations. Depends only on Domain.   |
| **Infrastructure** | Data access logic (JSON file reading). Depends on Application and Domain. |
| **Api**            | Controllers, endpoints, and startup configuration (DI, Swagger, etc.).    |
| **Tests**          | Unit and integration tests for services and repositories.                 |

---

## ⚙️ Technologies

- .NET 8 Web API
- C# 12
- System.Text.Json (for deserialization)
- Dependency Injection (built-in)
- Swagger (Swashbuckle)
- xUnit + Moq (testing framework)
- Follows Clean Architecture conventions

---

## 📂 Static JSON Data

The API must load data from a static JSON file located at:

src/SimpleCoffee.Infrastructure/Data/coffee-data.json

### 📄 coffee-data.json

```json
[
  {
    "id": 1,
    "name": "Cappuccino",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/cappuccino.jpg",
    "price": "$5.20",
    "rating": 4.7,
    "votes": 65,
    "popular": true,
    "available": true
  },
  {
    "id": 2,
    "name": "House Coffee",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/house-coffee.jpg",
    "price": "$3.50",
    "rating": 4.85,
    "votes": 15,
    "popular": true,
    "available": true
  },
  {
    "id": 3,
    "name": "Espresso",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/espresso.jpg",
    "price": "$2.50",
    "rating": 4.9,
    "votes": 55,
    "popular": false,
    "available": true
  },
  {
    "id": 4,
    "name": "Coffee Latte",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/coffee-latte.jpg",
    "price": "$4.50",
    "rating": 5.0,
    "votes": 23,
    "popular": false,
    "available": true
  },
  {
    "id": 5,
    "name": "Chocolate Coffee",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/chocolate-coffee.jpg",
    "price": "$4.00",
    "rating": "4.65",
    "votes": 122,
    "popular": false,
    "available": false
  },
  {
    "id": 6,
    "name": "Valentine Special",
    "image": "https://csyxkpbavpcrhwqhcpyy.supabase.co/storage/v1/object/public/assets/coffee-challenge/valentine-special.jpg",
    "price": "$5.50",
    "rating": null,
    "votes": 0,
    "popular": false,
    "available": true
  }
]
```

Core Entity
Coffee.cs

```cs
public class Coffee
{
public int Id { get; set; }
public string Name { get; set; } = string.Empty;
public string Image { get; set; } = string.Empty;
public string Price { get; set; } = string.Empty;
public double? Rating { get; set; }
public int Votes { get; set; }
public bool Popular { get; set; }
public bool Available { get; set; }
}
```

Application Layer
ICoffeeService.cs

```cs
public interface ICoffeeService
{
Task<IEnumerable<Coffee>> GetAllAsync();
Task<Coffee?> GetByIdAsync(int id);
Task<IEnumerable<Coffee>> GetPopularAsync();
Task<IEnumerable<Coffee>> GetAvailableAsync();
}
```

Infrastructure Layer
CoffeeRepository.cs

Reads the JSON file from /Data/coffee-data.json.

Deserializes it using System.Text.Json.

Provides data access methods for all coffees and filtering.

🌐 API Endpoints
Method Route Description
GET /api/coffee Returns all coffee items
GET /api/coffee/{id} Returns details of a coffee by ID
GET /api/coffee/popular Returns coffees marked as popular
GET /api/coffee/available Returns coffees currently available

dotnet restore
dotnet build
dotnet run --project src/SimpleCoffee.Api

http://localhost:5184
dotnet test

Include tests for:

- JSON data deserialization

- Filtering logic (popular, available)

- Service and repository behavior

License

MIT License © 2025 Juan Sebastián Restrepo Nieto
