Bug Management Dashboard

A web-based Bug Management System built with .NET 8, Carter Minimal APIs, MediatR, FluentValidation, and Angular, following Clean Architecture and modular design principles.

🚀 Features

Create, update, delete, and view bugs

Bug statuses:

Open

Work In Progress

Hold

Closed

Rejected

Pagination & search support

Input validation using FluentValidation

Centralized exception handling

Structured logging with Serilog

Modular architecture

RESTful APIs

🧱 Tech Stack
Backend

.NET 8

Carter (Minimal APIs)

MediatR (CQRS pattern)

Entity Framework Core

MySQL

FluentValidation

Serilog

Frontend

Angular (CRUD dashboard UI)

📁 Project Structure
BugManagement
├── UI                          # API Host
│   ├── Middleware              # Global exception handling
│   └── Program.cs
│
├── Modules.BugManagement       # Feature module
│   ├── Bugs
│   │   ├── Endpoints           # Carter endpoints
│   │   ├── Features            # Commands & Queries
│   │   ├── Validators          # FluentValidation validators
│   │   └── ViewModels
│   │
│   ├── Shared
│   │   ├── Data
│   │   │   ├── Context
│   │   │   └── Repositories
│   │   └── Domain
│
├── Shared                      # Cross-cutting concerns
│   ├── Filters                 # Endpoint filters (ValidatorFilter)
│   ├── Models                  # Response & validation models
│   └── Extensions              # DI extensions

🔄 Request Flow
Client Request
   ↓
Carter Endpoint
   ↓
ValidatorFilter<T> (FluentValidation)
   ↓
MediatR Command / Query
   ↓
Repository (EF Core)
   ↓
Database

🧪 Validation

Validation is applied at endpoint level using IEndpointFilter

Invalid requests return 400 Bad Request

Handlers are kept free of validation logic

Example:

app.MapPost(string.Empty, Create)
   .AddEndpointFilter<ValidatorFilter<CreateBugCommand>>();

📄 Pagination & Search

A shared request model is used for pagination and search:

public record SearchRequest
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
    public string? Search { get; init; }
}


Pagination is applied at database level using Skip and Take.

🧾 Logging

Serilog is used for structured logging

Errors and exceptions are logged to file

Info and warnings are logged to console

Logs/log.txt

⚠️ Global Exception Handling

All unhandled exceptions are captured in a single middleware:

app.UseMiddleware<GlobalExceptionMiddleware>();


Prevents application crashes

Returns clean error responses

Logs full stack traces

🔧 Configuration
Database Connection

Update appsettings.json:

"ConnectionStrings": {
  "BugDb": "server=localhost;database=bugdb;user=root;password=yourpassword"
}

▶️ Running the Project
Backend
dotnet restore
dotnet run


API will be available at:

http://localhost:5091


Swagger UI:

http://localhost:5091/swagger

🧠 Design Decisions

CQRS using MediatR

Feature-based modular architecture

Endpoint-level validation

No exceptions for expected business cases

Centralized logging & error handling

📌 Future Improvements

Authentication & authorization

Role-based access

Advanced filtering & sorting

UI enhancements

Unit & integration tests

👤 Author

Hemanth

.NET Backend Developer
Specialized in ASP.NET Core, Web APIs, Clean Architecture, and scalable backend systems.
