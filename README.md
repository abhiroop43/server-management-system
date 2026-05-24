# Server Management System

[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE.txt)
[![.NET Version](https://img.shields.io/badge/.NET-10.0-purple)](https://dotnet.microsoft.com)

![GitHub Repo Banner](https://ghrb.waren.build/banner?header=%21%5Bopensourcehardware%5D+Server+Management+%F0%9F%92%BD&subheader=Enterprise-grade+Server+Management+Solution&bg=013B84-016EEA&color=FFFFFF&headerfont=Permanent+Marker&subheaderfont=Kinewave)

A modern, full-stack server management system built with .NET 10, featuring a REST API backend and an interactive Blazor web frontend. Manage servers, disks, and services with user authentication, role-based access control, and a responsive UI.

## Table of Contents

- [Features](#features)
- [Technology Stack](#technology-stack)
- [Project Structure](#project-structure)
- [Prerequisites](#prerequisites)
- [Getting Started](#getting-started)
- [Development](#development) <!-- - [Architecture](#architecture) -->
- [Support & Documentation](#support--documentation)
- [Contributing](#contributing)
- [License](#license)

## Features

- **Server Management**: Create, read, update, and delete server resources
- **Disk Management**: Monitor and manage server disk storage
- **Service Monitoring**: Track hosted services running on servers
- **User Authentication**: Secure JWT-based authentication system
- **User Management**: User registration, login, password reset, and profile updates
- **Email Integration**: Email confirmation and password reset notifications
- **Health Checks**: Built-in health monitoring for API and database connectivity
- **API Documentation**: Auto-generated OpenAPI/Swagger documentation
- **Clean Architecture**: Domain-driven design with CQRS pattern
- **Input Validation**: Fluent validation with automatic validation behaviors
- **Structured Logging**: Comprehensive logging with Serilog

## Technology Stack

### Backend

- **.NET 10**: Latest .NET runtime
- **ASP.NET Core**: Web API framework
- **Entity Framework Core**: ORM for SQL Server
- **MediatR**: CQRS pattern implementation
- **Carter**: Minimal API routing
- **FluentValidation**: Data validation library
- **Serilog**: Structured logging
- **JWT Bearer**: Authentication & authorization

### Frontend

- **Blazor Web App**: Interactive web UI framework
- **MudBlazor**: Material Design component library

### Data & Infrastructure

- **SQL Server**: Primary database (LocalDB for development)
- **Entity Framework Core Migrations**: Database versioning

## Project Structure

```text
ServerManagement.sln
├── API/
│   └── ServerManagement.API/          # REST API layer
│       ├── Features/
│       │   ├── Auth/                  # Authentication endpoints
│       │   └── Users/                 # User management endpoints
│       ├── Program.cs                 # Application entry point
│       ├── DependencyInjection.cs     # Service registration
│       └── ServerManagement.API.csproj
├── Domain/
│   └── ServerManagement.Domain/       # Business logic layer
│       ├── Entities/                  # Domain models (Server, Disk, HostedService)
│       ├── CQRS/                      # Commands & Queries
│       ├── Behaviors/                 # Validation & logging behaviors
│       ├── Abstractions/              # Base classes for DDD
│       ├── Exceptions/                # Domain exceptions
│       └── ServerManagement.Domain.csproj
├── Infrastructure/
│   └── ServerManagement.Infrastructure/  # Data access layer
│       ├── Data/                      # DbContext configurations
│       ├── Auth/                      # Identity & security
│       ├── Services/                  # External service integrations
│       ├── Migrations/                # Database migrations
│       └── ServerManagement.Infrastructure.csproj
├── UI/
│   └── ServerManagement.UI/           # Blazor web interface
│       ├── Components/                # Reusable Blazor components
│       ├── Pages/                     # Application pages
│       ├── Models/                    # View models
│       └── ServerManagement.UI.csproj
└── LICENSE.txt
```

## Prerequisites

- **.NET 10 SDK** or later
- **SQL Server** (Express, LocalDB, or Azure SQL)
- **Visual Studio 2024** or **Visual Studio Code** with C# extension
- **Node.js** (optional, for advanced frontend tooling)

## Getting Started

### Installation

1. **Clone the repository**

   ```bash
   git clone https://github.com/your-username/server-management-system.git
   cd server-management-system
   ```

2. **Restore NuGet packages**

   ```bash
   dotnet restore
   ```

3. **Configure the database connection** (if not using LocalDB)
   - Edit `API/ServerManagement.API/appsettings.json`
   - Update the `ConnectionStrings:ServerManagement` connection string to point to your SQL Server instance

4. **Apply database migrations**

   ```bash
   dotnet ef database update --project Infrastructure/ServerManagement.Infrastructure --startup-project API/ServerManagement.API
   ```

5. **Configure user secrets for development** (optional but recommended)

   ```bash
   dotnet user-secrets set "MEDIATR_LICENSE_KEY" "your-license-key" --project API/ServerManagement.API
   ```

### Running the Application

#### Using .NET CLI

**Terminal 1 - Start the API server** (port 5120):

```bash
cd API/ServerManagement.API
dotnet run
```

**Terminal 2 - Start the UI server** (port 5254):

```bash
cd UI/ServerManagement.UI
dotnet run
```

#### Using Visual Studio

1. Open `ServerManagement.slnx` in Visual Studio
2. Right-click on the solution → **Properties** → **Startup Project** → Select **Multiple startup projects**
3. Set both `ServerManagement.API` and `ServerManagement.UI` to **Start**
4. Press **F5** or click **Debug** → **Start Debugging**

#### Using Visual Studio Code

1. Install the C# extension and .NET runtime
2. Open the project folder
3. Press **F5** to start debugging (uses `launch.json` configuration)

### Verifying Installation

Once both servers are running:

- **API**: Navigate to `http://localhost:5120` for OpenAPI documentation
- **UI**: Navigate to `http://localhost:5254` to access the web interface
- **Health Check**: Visit `http://localhost:5120/health` to verify API health

## Development

### Project Architecture

This application follows **Clean Architecture** principles:

- **Domain Layer**: Contains business logic, domain entities, and CQRS contracts
- **Infrastructure Layer**: Implements data access, authentication, and external services
- **API Layer**: Exposes REST endpoints using Carter for minimal API routing
- **UI Layer**: Provides the user interface via Blazor Web App

### CQRS Pattern

The application implements Command Query Responsibility Segregation (CQRS):

- **Commands**: Operations that modify state (e.g., `CreateServerCommand`)
- **Queries**: Operations that retrieve data (e.g., `GetServerQuery`)
- **Handlers**: Implement business logic and validation
- **Behaviors**: Cross-cutting concerns like logging and validation

### Running Tests

```bash
dotnet test
```

### Building for Production

```bash
dotnet publish -c Release -o ./publish
```

### Database Migrations

**Create a new migration**:

```bash
dotnet ef migrations add MigrationName --project Infrastructure/ServerManagement.Infrastructure --startup-project API/ServerManagement.API
```

**Apply migrations**:

```bash
dotnet ef database update --project Infrastructure/ServerManagement.Infrastructure --startup-project API/ServerManagement.API
```

## Support & Documentation

### API Documentation

Once the API is running, interactive API documentation is available at:

- **OpenAPI/Swagger UI**: `http://localhost:5120`

### Logging

The application uses Serilog for structured logging. Logs are output to the console during development.

### Health Checks

Monitor application health and dependencies:

```bash
curl http://localhost:5120/health
```

### Getting Help

- **Issues**: If you encounter bugs or have feature requests, please [open an issue](https://github.com/your-username/server-management-system/issues)
- **Discussions**: For questions and community discussions, use [GitHub Discussions](https://github.com/your-username/server-management-system/discussions)

## Contributing

We welcome contributions! Please follow these guidelines:

1. **Fork** the repository
2. **Create a feature branch**: `git checkout -b feature/your-feature-name`
3. **Make your changes** following the existing code style
4. **Commit** with clear, descriptive messages: `git commit -m "Add feature: description"`
5. **Push** to your fork: `git push origin feature/your-feature-name`
6. **Open a Pull Request** with a description of your changes

For detailed contribution guidelines, see [CONTRIBUTING.md](CONTRIBUTING.md) (when available).

### Code Style

- Follow [C# coding conventions](https://learn.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions)
- Use nullable reference types (`#nullable enable`)
- Use implicit usings (`using global::`)
- Write clear, self-documenting code

## License

This project is licensed under the **MIT License** - see [LICENSE.txt](LICENSE.txt) for details.

---

**Maintainer**: [Abhiroop Santra](https://github.com/abhiroopsantra)
**Last Updated**: May 2026
