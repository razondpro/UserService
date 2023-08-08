# UserService Microservice

UserService is a microservice built using C# .NET Minimal API, following the Clean Architecture and Domain-Driven Design (DDD) principles. It integrates Kafka for event handling and Postgressql as the database.

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

UserService is a microservice that handles user-related operations. It is built with C# .NET Minimal API, adopting the Clean Architecture and Domain-Driven Design (DDD) patterns to ensure a modular, maintainable, and scalable codebase. The service also leverages Kafka for event-driven communication and MongoDB for data storage.

## Features

- Create, update, and manage user profiles
- Event-driven architecture using Kafka for real-time updates
- MongoDB integration for data storage and retrieval
- Clean and modular codebase following DDD principles
- Efficient and lightweight with C# .NET Minimal API

## Requirements

To run this microservice, you'll need the following tools and technologies:

- .NET SDK (version XYZ)
- Kafka (version XYZ)
- MongoDB (version XYZ)

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

## Configuration

1. Configure Kafka:
   Update the Kafka settings in appsettings.json to match your Kafka broker configuration.

2. Configure MongoDB:
   Update the MongoDB connection string in appsettings.json to point to your MongoDB instance.

## Usage

1. Run the microservice:

```bash
    dotnet run
```

2. Access the API at http://localhost:5000.

## Contributing

Contributions are welcome! If you'd like to contribute to UserService, follow these steps:

1. Fork the repository.
2. Create a new branch for your feature/bugfix.
3. Make your changes and write tests if needed.
4. Ensure all tests pass:

```bash

   dotnet test
```

5. Create a pull request.

## License

This project is licensed under the XYZ License. See the LICENSE file for details.
