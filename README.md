# Workspace Management System (ASP.NET Core Web API)

## Overview
Workspace Management System (WMS) is a comprehensive ASP.NET Core Web API application focused on managing workspaces, rooms, client bookings, products (snacks/beverages), and automated invoicing. The system emphasizes Clean Architecture, separation of concerns, relational data modeling, and robust business logic using the Database First approach. 

## Objectives
- Apply Clean Architecture principles by separating Domain, Application, Infrastructure, and Presentation layers
- Model relational data using Database First and Fluent API
- Implement full CRUD operations for Rooms, Products, and Room Rates
- Manage dynamic workspace bookings and real-time product additions
- Streamline invoice generation linking bookings to products
- Implement secure user authentication and role-based authorization using JWT
- Provide well-documented RESTful API endpoints via Swagger

## Technologies Used
- **Language**: C#
- **Framework**: ASP.NET Core Web API (.NET 8.0)
- **Architecture**: Clean Architecture, Repository Pattern, Unit of Work
- **ORM**: Entity Framework Core (Database First)
- **Database**: SQL Server
- **Authentication**: ASP.NET Core Identity & JWT (JSON Web Tokens)
- **Tools**: Visual Studio, Git, GitHub, Swagger

## System Architecture
The project strictly follows the Clean Architecture pattern to ensure maintainability and testability:

- **Domain Layer (`Workspace.Domain`)**: Contains enterprise-wide logic and Database Entities (`TbRoom`, `TbBooking`, `TbInvoice`, `TbProduct`, etc.).
- **Application Layer (`Workspace.Application`)**: Contains core business rules, Services (`BookingServices`, `InvoiceServices`), DTOs, interfaces, and system constants.
- **Infrastructure Layer (`Workspace.Infrastructure`)**: Handles external concerns, containing the Entity Framework `DbContext` (`WorkSpaceSysContext`), implementations of Repositories (`RoomRepo`, `BookingRepo`, etc.), and the Unit of Work (`UnitOfWork`).
- **Presentation Layer (`Workspace Management System`)**: The REST API application containing Controllers, global Exception Middleware, JWT handling, and Dependency Injection configurations.

## Core Features

### Room & Workspace Management
- Create, update, and retrieve details of available workspace rooms.
- Manage dynamic room rates based on times or types.

### Booking System
- Create and manage room bookings.
- Track ongoing, non-completed, and finalized bookings.
- Add products/snacks seamlessly to active bookings before completion.

### Product Management
- Manage inventory of products and snacks available in the workspace.
- Monitor product availability for clients.

### Invoice Generation & Statistics
- Complete active bookings to automatically generate comprehensive invoices.
- Invoices calculate the combined cost of utilized room time and consumed products.
- View total and daily administrative profit statistics.

### Status & System Configuration
- Manage system settings globally.
- Control operational statuses dynamically using lookups.

### User Authentication
- Secure JWT-based authentication.
- User registration and login flows.
- Role-based authorization (Admin/Customer roles automatically seeded on startup).
- Password policies and validation rules.

## Database Design

### Entity Relationships
- **Room → RoomRate**: One-to-Many
- **Room → Booking**: One-to-Many 
- **Booking ↔ Product**: Many-to-Many (Resolved via `TbBookingProduct`)
- **Invoice ↔ Booking**: Many-to-Many (Resolved via `TbInvoiceBooking`)
- **StatusType → Status**: One-to-Many

Relationships are configured via the Database First approach ensuring data integrity constraints directly match the underlying SQL database schemas.

## Project Structure

```text
Workspace Management System.sln
├── Workspace Management System/     # Presentation Layer (Web API)
│   ├── Controllers/                 # REST API Endpoints (e.g., StatusController)
│   ├── Middlewares/                 # Global error handling (ExceptionMiddleware)
│   └── Program.cs                   # App configuration, DI, and Swagger & JWT Setup
├── Workspace.Application/           # Application Layer
│   ├── DTOs/                        # Request and Response Data Transfer Objects
│   ├── Interfaces/                  # Service & Repository abstractions
│   └── Implementations/             # Core Business logic (e.g., InvoiceServices)
├── Workspace.Domain/                # Domain Layer
│   └── Entities/                    # Database-first models (TbInvoice, TbRoom, etc.)
└── Workspace.Infrastructure/        # Infrastructure Layer
    ├── Data/                        # EF Core DbContext
    └── Repositories/                # Repository Pattern & Unit of Work implementations
```

## Validation & Data Integrity
- Requests validated strictly through Application-layer DTOs.
- Centralized exception handling via custom middleware for uniform API error responses.
- Relational integrity rules enforced at the database level and mapped via Entity Framework.
- JWT Security enforcing strict Issuer, Audience, and token Lifetime validations.

## Learning Outcomes
- Practical implementation of Clean Architecture in .NET 8.
- Mastering the Repository and Unit of Work design patterns.
- Generating robust code via EF Core Database First approach.
- Securing REST APIs using Identity and JSON Web Tokens.
- Orchestrating complex relational business logic for bookings and invoicing.

## Getting Started

### Prerequisites
- Visual Studio 2022 or later
- .NET 8.0 SDK or later
- SQL Server (LocalDB or full instance)

### Installation
1. Clone the repository:
   ```bash
   git clone https://github.com/your-username/Workspace-Management-System.git
   ```
2. Open `Workspace Management System.sln` in Visual Studio.
3. Update the connection string named `conn` in `appsettings.json` within the Web API project to point to your existing SQL Server database.
4. Run the project (typically mapping to Swagger UI for endpoint testing).
   ```bash
   dotnet run
   ```
