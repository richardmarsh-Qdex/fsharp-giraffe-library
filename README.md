# Library Management System - F# + Giraffe

A library management system built with F# and Giraffe framework.

## Features

- Book management (CRUD)
- Member management
- Loan tracking
- Return processing
- RESTful API endpoints

## Installation

```bash
dotnet restore
```

## Running

```bash
# Development
dotnet run

# Production
dotnet build
dotnet publish
```

## API Endpoints

- GET /api/books - Get all books
- POST /api/books - Create book
- GET /api/books/:id - Get book by ID
- PUT /api/books/:id - Update book
- DELETE /api/books/:id - Delete book
- GET /api/members - Get all members
- POST /api/members - Create member
- GET /api/members/:id - Get member by ID
- GET /api/loans - Get all loans
- POST /api/loans - Create loan
- POST /api/loans/:id/return - Return loan

## Tech Stack

- F#
- Giraffe Framework
- ASP.NET Core
- .NET 8
