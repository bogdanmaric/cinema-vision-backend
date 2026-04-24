# 🎬 Cinema Vision API

Backend REST API for the Cinema Vision movie application.

Provides user authentication, movie search integration with OMDb API, movie details, and personal favorites management.

Built with **ASP.NET Core Web API**, **Entity Framework Core**, **ASP.NET Identity**, **JWT Authentication**, and **PostgreSQL**.

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

### 🛠 Quality
- DTO-based request and response models
- Input validation with DataAnnotations
- Logging with ASP.NET Core ILogger
- Error handling for database operations
- Layered architecture (Controllers, Services, DTOs, Data)

---

## 🧱 Tech Stack

- ASP.NET Core Web API
- Entity Framework Core
- ASP.NET Identity
- JWT Bearer Authentication
- PostgreSQL
- OMDb API

---

## 📁 Project Structure

```text
MovieApi/
├── Controllers/
├── Data/
├── Dtos/
├── Models/
├── Services/
├── Migrations/
└── Program.cs
````

---

## ⚙️ Setup Instructions

### 1. Clone repository

```bash
git clone <your-repository-url>
cd cinema-vision-api
```

### 2. Configure database connection

Update `appsettings.json`

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=MovieDb;Username=your_user;Password=your_password"
}
```

### 3. Configure JWT settings

```json
"Jwt": {
  "Key": "your_secret_key",
  "Issuer": "your_issuer",
  "Audience": "your_audience"
}
```

### 4. Configure OMDb API key

```json
"Omdb": {
  "ApiKey": "your_api_key"
}
```

### 5. Apply migrations

```bash
dotnet ef database update
```

### 6. Run the API

```bash
dotnet run
```

---

## 🔌 API Endpoints

### Auth

* `POST /api/auth/register`
* `POST /api/auth/login`

### Movies

* `GET /api/movies/search?title=batman`
* `GET /api/movies/{id}`

### Favorites (Authorized)

* `GET /api/favorites`
* `POST /api/favorites`
* `DELETE /api/favorites/{id}`

---

## 🧠 Concepts Demonstrated

* RESTful API development
* JWT authentication and authorization
* ASP.NET Identity integration
* Entity relationships with EF Core
* External API consumption
* DTO pattern
* Validation with DataAnnotations
* Structured logging
* Error handling
* PostgreSQL integration

---

## 💻 Frontend Repository

Frontend client available in separate repository:

`cinema-vision-client`

---

## 👤 Author

Full-stack developer focused on building clean, secure, and practical web applications.

---

## 📄 License

This project is for educational and portfolio purposes.

```
```
