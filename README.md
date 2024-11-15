# WarehouseEnjoyments

## Overview

**WarehouseEnjoyments** is a sample project combining a backend built using ASP.NET Core Web API and a frontend built using Angular.

The purpose of this project is to explore clean architecture and a domain-driven design approach for winning over business-driven needs and problems. It contains enjoyments of concepts like clean architecture, vertical slicing, and domain-driven design (DDD) within ASP.NET Core.

## Tech Enjoyed In This Project

- ASP.NET Core 6
- Entity Framework Core
- MediatR
- AutoMapper
- FluentValidation
- FluentAssertions
- OData
- NUnit
- Moq
- Respawn
- Swagger
- Angular
- Bootstrap

## Solution content

### Backend

- **API Layer**: Entry point for all client requests.
- **Infrastructure Layer**: Contains data access implementations, startup configurations, and cross-cutting concerns.
- **Application Layer**: Business logic, service definitions, and DTOs, separated from presentation and infrastructure layers for flexibility and reusability.
- **Domain Layer**: Core business entities, value objects, and domain-specific logic.

### Frontend

- **Angular Application**: Structured with components, services, and modules for maintainability and modularity.

## Key concepts enjoyed

### Backend Paradigms

- **Clean Architecture**: Ensures a separation of concerns, allowing for scalable and maintainable code by structuring the project into distinct layers.

- **Vertical Slicing**: Each feature is developed independently, maintaining its own layers for easier code navigation and maintenance.

- **Domain-Driven Design (DDD)**: Implements protected properties, property operations, constraint creation, required/length attributes, and value objects to encapsulate domain logic.

- **Paging, Ordering, and Filtering**:
	- Custom logic for API responses.
	- OData compliance.
	- Paged query/response models.
	- AutoMapper expression mapping for efficient data transformations.

- **Unit Testing**:
	- Utilizes NUnit and Moq for service and route testing.
	- Respawn manages database state during testing.

- **Infrastructure Layer**:
	- Startup modularization for maintainable configuration.

- **Features highlights**:
	- `init-only` setters and records in C#.
	- Configuration registration and retrieval via `IOptions<>`.
	- Conditional validations using FluentValidation.

- **Repository Layer**:
	- `ApplicationDbContext` for persistence
	- Repositories for data retrieval, including cross-cutting and SQL-specific queries.

- **CQRS**: Simplified folder structure to organise commands and queries efficiently.

### Frontend Paradigms

- **Custom Validators**: Ensures input validation through tailored Angular directives.

- **Fully Utilized Angular**: Uses Angular's full capabilities for building a responsive, user-friendly interface.

- **Bootstrap Integration**: Enhances UI styling for a modern, clean design.

## How to really enjoy this repository

### backend:

1. Ensure the `ConnectionStrings` section in `appsettings.json` is configured according to your database setup.
2. Run the backend using:
`dotnet run`
3. Enjoy!

### frontend:

**Prerequisites:**
-  NodeJs and npm
	- Ensure you have NodeJs and npm using:
		-  `node -v`
		-  `npm -v`
	- Angular CLI
		- install the angular CLI globally using:
		- `npm install -g @angular/cli@11`
   
1. Open your terminal and navigate to the root of the directory of the Angular project
	- directory for this project should be 
		- `root/src/WebUI`
2. Run the command `npm install`
3. Run the command `ng serve --open`
4. Enjoy!

## Resources I have enjoyed

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)

- [Designing a DDD-oriented microservice - .NET](https://learn.microsoft.com/en-us/dotnet/architecture/microservices/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)

- [How to Structure Your Domain Driven Design Project in ASP.NET Core](https://medium.com/@cizu64/how-to-structure-your-domain-driven-design-project-in-asp-net-core-dbec0cc0ce53)

- [ASP.NET 6 REST API Following CLEAN ARCHITECTURE & DDD Tutorial](https://youtube.com/playlist?list=PLzYkqgWkHPKBcDIP5gzLfASkQyTdy0t4k&si=DXv3I72KVyvb467F)


## Concluding..
**WarehouseEnjoyments** will help you as a developer to explore ASP.NET Core Web API integration with Angular on your own and how you can leverage **best practices for modern development, focusing on clean architecture, vertical slicing, and domain-driven design** to build maintainable and testable applications.


Thank you for reading!😊