# UserService Microservice

The core focus of this project is to offer guidance on the principles of software application design. This encompasses a compilation of techniques, tools, best practices, architectural patterns, and guidelines sourced from various references.

The patterns and principles outlined here are independent of any specific programming language or framework. Therefore, these recommendations can be applied universally, regardless of the technologies chosen for implementation.

It's important to emphasize that the guidance provided here is not intended as strict rules, but rather as a set of recommendations. Different projects have distinct requirements, and the application of these patterns should be tailored to meet the unique needs of each project. In real-world production applications, only a subset of these patterns may be necessary, depending on specific use cases. For more details, please refer to the subsequent section.

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Requirements](#requirements)
- [Installation](#installation)
- [Configuration](#configuration)
- [Usage](#usage)
- [Contributing](#contributing)
- [License](#license)

## Introduction

This project serves as a versatile starting point for developers, offering a well-structured and modular codebase that adheres to best practices and industry-standard architectural patterns.

UserService is built using C# .NET Minimal API, and it embraces the principles of Clean Architecture and Domain-Driven Design (DDD). By adopting these patterns, the project ensures that your code remains maintainable and scalable, regardless of the size and complexity of your application.

In addition, UserService leverages Kafka, a powerful event streaming platform, for event-driven communication between components of your application. This asynchronous approach enhances responsiveness and flexibility, providing developers with the tools they need to create, maintain, and scale applications with ease.

Whether you're embarking on a small-scale project or tackling a large enterprise application, UserService is your trusted companion for user operations. It offers an open-source, solid, and well-structured template for your software development endeavors, ensuring you start with a strong foundation for your unique project."

## Features

- Create, update user profiles using Minimal API with proper validation and error handling
- Domain Events with Outbox Pattern: Implements domain events using the Outbox Pattern, ensuring reliable and idempotent event processing. This pattern ensures that events are processed exactly once, even if there are network issues or failures, preventing duplicate events and maintaining data integrity.
- Asynchronous Event Communication: Utilizes Kafka for asynchronous event-driven communication with other microservices, allowing real-time updates and seamless integration between components of the application.
- Reliable Data Handling with EF Core: Integrates PostgreSQL (PSQL) with Entity Framework Core (EF Core) to ensure secure, responsive, and efficient data storage and retrieval.
- Modular Codebase: Follows Clean Architecture principles, resulting in a well-structured, modular codebase that enhances code maintainability and scalability.
- Efficiency with Minimal API: Developed using C# .NET Minimal API to provide an efficient and lightweight solution, promoting speed and code conciseness.

## Requirements

To run this microservice, you'll need the following tools and technologies:

- Docker
- .NET SDK (version 7.0)

## Installation

1. Clone the repository:

```bash
   git clone <rhttps://github.com/razondpro/UserService.git>
   cd UserService
```

2. Install dependencies and build the project:

```bash
    dotnet restore
    dotnet build
```

3. Docker Compose

```bash
    docker-compose up -d
```

## Configuration

1. Migrations

```bash
    dotnet ef migrations add InitialCreate
    dotnet ef database update
```

## Usage

1. Run the microservice:

```bash
    dotnet run
```

2. Access the API at http://localhost:5290/swagger/index.html
