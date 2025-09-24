# DeliverySystem API

Simple RESTful API for managing deliveries, built with ASP.NET Core and PostgreSQL.

## Project Status
🚧 **Under Development** 

This project is in active development. Core features are being implemented, and the API structure may change.

## Features

Current:
- CRUD operations for:
  - Customers
  - Couriers
  - Orders
- Order status tracking
- Courier status management

Planned:
- Authentication and Authorization using JWT tokens
- FluentValidation for request validation
- Role-based access control
- Secure password handling
- Request rate limiting
- API versioning

## Tech Stack

Current:
- ASP.NET Core 9.0
- PostgreSQL
- Entity Framework Core
- Docker
- Swagger/OpenAPI

Planned:
- JWT Authentication
- FluentValidation
- Identity Framework

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Docker and Docker Compose
- PostgreSQL (if running locally)

### Running with Docker

1. Clone the repository:
```bash
git clone https://github.com/Choffel/DeliverySystem.git
cd DeliverySystem
```

2. Run with Docker Compose:
```bash
docker compose up --build
```

The API will be available at `http://localhost:8080`  
Swagger UI: `http://localhost:8080/swagger`

### Running Locally

1. Update connection string in `appsettings.json` if needed
2. Run the migrations:
```bash
dotnet ef database update
```
3. Start the application:
```bash
dotnet run
```

## Database Configuration

The project supports two database configurations:

### Local Development
- PostgreSQL running locally
- Connection string in `appsettings.json`:
```json
"DefaultConnection": "Host=localhost;Port=5432;Database=DeliveryDb;Username=postgres;Password=123"
```

### Docker Environment
- PostgreSQL container as part of docker-compose
- Connection configured through environment variables in `.env`:
```env
POSTGRES_HOST=db
POSTGRES_PORT=5432
POSTGRES_DB=DeliveryDb
POSTGRES_USER=postgres
POSTGRES_PASSWORD=123
```

Choose the appropriate configuration based on your development environment:
- For local development: Use local PostgreSQL and `appsettings.json`
- For containerized environment: Use Docker Compose with provided `.env`

## API Endpoints

### Customers
- `GET /api/customers` - Get all customers
- `GET /api/customers/{id}` - Get customer by ID
- `POST /api/customers` - Create new customer
- `PUT /api/customers/{id}` - Update customer
- `DELETE /api/customers/{id}` - Delete customer

### Couriers
- `GET /api/couriers` - Get all couriers
- `GET /api/couriers/{id}` - Get courier by ID
- `POST /api/couriers` - Create new courier
- `PUT /api/couriers/{id}` - Update courier
- `DELETE /api/couriers/{id}` - Delete courier

### Orders
- `GET /api/delivery` - Get all orders
- `GET /api/delivery/{id}` - Get order by ID
- `POST /api/delivery` - Create new order
- `PUT /api/delivery/{id}` - Update order
- `DELETE /api/delivery/{id}` - Delete order

## Project Structure

```
DeliverySystem/
├── Controllers/         # API Controllers
├── Models/             # Domain Models
├── Services/           # Business Logic
├── Data/              # Database Context
├── DTOs/              # Data Transfer Objects
├── Enums/             # Enumerations
└── Abstractions/      # Interfaces
```

## Contributing

As this project is under development, contributions are welcome. Feel free to open issues and submit pull requests.

## License

This project is open source and available under the [MIT License](LICENSE).

## Note

This is a development version. Database credentials and other sensitive information should be properly secured before production deployment.
