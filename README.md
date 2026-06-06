# 🎬 Cinema Vision API

Backend REST API for the Cinema Vision movie application.

Provides user authentication, movie search integration with the OMDb API, movie details, and personal favorites management.

Built with **ASP.NET Core Web API**, **Entity Framework Core**, **ASP.NET Identity**, **JWT Authentication**, **PostgreSQL**, and **Docker**.

---

## 🎥 Demo

[![Watch Demo](https://img.youtube.com/vi/2yxXLsC6Q9k/maxresdefault.jpg)](https://www.youtube.com/watch?v=2yxXLsC6Q9k)

---

## 🚀 Features

### 🔐 Authentication
- User registration
- User login
- JWT token authentication
- Protected endpoints for authorized users

### 🎥 Movies
- Search movies by title using OMDb API
- Get detailed movie information by IMDb ID

### ⭐ Favorites
- Add movies to favorites
- View personal favorites list
- Remove favorite movies
- Prevent duplicate favorites per user

### 🛠 Quality & Architecture
- DTO-based request and response models
- Input validation with DataAnnotations
- Logging with ASP.NET Core ILogger
- Global exception handling middleware
- Standardized API error responses
- Layered architecture (Controllers, Services, DTOs, Data)
- Service layer abstraction
- Unit testing with xUnit and Moq
- Automatic EF Core migrations on startup
- Dockerized backend and database setup
- CORS configuration for frontend integration

---

## 🧱 Tech Stack

- ASP.NET Core Web API (.NET 10)
- Entity Framework Core
- ASP.NET Identity
- JWT Bearer Authentication
- PostgreSQL
- Docker & Docker Compose
- xUnit
- Moq
- FluentAssertions
- OMDb API

---

## 📁 Project Structure

```text
backend/
├── docker-compose.yml
├── Dockerfile
└── MovieApi/
    ├── MovieApi/
    │   ├── Controllers/
    │   ├── Data/
    │   ├── Dtos/
    │   ├── Interfaces/
    │   ├── Middleware/
    │   ├── Models/
    │   ├── Services/
    │   ├── Migrations/
    │   └── Program.cs
    │
    ├── MovieApi.Tests/
    └── MovieApi.slnx
```

---

## ⚙️ Setup Instructions

### 1. Clone repository

```bash
git clone <your-repository-url>
cd cinema-vision/backend
```

---

### 2. Configure database connection

Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=movie_db;Username=movie_user;Password=movieuser"
}
```

---

### 3. Configure JWT settings

```json
"Jwt": {
  "Key": "your_secret_key",
  "Issuer": "MovieApi",
  "Audience": "MovieApiUsers",
  "DurationInMinutes": 60
}
```

---

### 4. Configure OMDb API key

```json
"Omdb": {
  "ApiKey": "your_api_key"
}
```

---

### 5. Apply migrations

```bash
dotnet ef database update
```

---

### 6. Run the API

```bash
dotnet run
```

API will be available at:

```text
http://localhost:5151
```

---

## 🐳 Run with Docker

### Start containers

```bash
docker-compose up --build
```

API will be available at:

```text
http://localhost:5000
```

### Stop containers

```bash
docker-compose down
```

The Docker setup includes:
- ASP.NET Core API container
- PostgreSQL database container
- Automatic EF Core migrations on startup

---

## 🔌 API Endpoints

### Auth

- `POST /api/auth/register`
- `POST /api/auth/login`

### Movies

- `GET /api/movies/search?title=batman`
- `GET /api/movies/{id}`

### Favorites (Authorized)

- `GET /api/favorites`
- `POST /api/favorites`
- `DELETE /api/favorites/{id}`

---

## ✅ Testing

Unit tests are implemented using:
- xUnit
- Moq
- FluentAssertions
- EF Core InMemory provider

Current tests cover:
- DTO validation
- JWT token generation
- Favorite movie service logic

Run tests with:

```bash
dotnet test
```

---

## 🧠 Concepts Demonstrated

- RESTful API development
- JWT authentication and authorization
- ASP.NET Identity integration
- Entity relationships with EF Core
- External API consumption
- DTO pattern
- Validation with DataAnnotations
- Structured logging
- Global exception handling
- Service layer architecture
- Dependency Injection
- Unit testing with xUnit and Moq
- Docker containerization
- PostgreSQL integration
- CORS configuration

---

## 💻 Frontend Repository

Frontend client available in separate repository:

`cinema-vision-frontend`

---

## 👤 Author

Full-stack developer focused on building clean, secure, and practical web applications.

---

## 📄 License

This project is for educational and portfolio purposes.
