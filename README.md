# Ambev Developer Evaluation - Sales Management

This project is a sample application for managing sales operations, including **Create**, **Read**, **Update**, and **Delete** (CRUD) operations for sales. It is built with **.NET 8**, **Entity Framework Core**, **MediatR**, **FluentValidation**, and **AutoMapper**.

---

## 📥 Setup

1. Clone the repository:

```bash
git clone https://github.com/yourusername/ambev-developer-evaluation.git
cd ambev-developer-evaluation
```

2. Restore NuGet packages:

```bash
dotnet restore
```

3. Configure the database connection string in `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=SalesDb;Trusted_Connection=True;"
}
```

4. Apply database migrations:

```bash
cd WebApi
dotnet ef database update
```

---

## 🚀 Running the Project

Start the Web API:

```bash
dotnet run --project WebApi
```

Once running, navigate to:

```
https://localhost:5001/swagger
```

to explore and test the endpoints via Swagger UI.

---

## 🧪 Running Tests

Unit tests are located in the `Unit` folder. To run all tests:

```bash
dotnet test
```

The tests cover:

- `CreateSaleHandler` (valid, invalid, missing product)
- `UpdateSaleHandler` (valid, invalid, sale not found, missing product)
- `DeleteSaleHandler` (valid, invalid, sale not found)
- ValueObjects validations (`ExternalCustomer`, `ExternalBranch`, `ExternalProduct`)

---

## 📚 Features

- **Sales CRUD** using CQRS pattern with MediatR
- **Domain validation** with FluentValidation
- **Event publishing** via domain events
- **Repository pattern** with EF Core
- **Unit tests** with xUnit, FluentAssertions, NSubstitute, Bogus

---

## 📝 Conventions

- **Entities** are immutable where possible; use methods to modify state
- **DTOs** are mapped via AutoMapper
- **Handlers** perform validation, business logic, persistence, and event publishing
- **Events** are published for creation, update, and deletion of sales

---

## 🔗 Repository Link

Once completed, share the public GitHub repository link for evaluation:

```
https://github.com/ViniciusAlberto/DeveloperEvaluation
```

---

## 👨‍💻 Author

- **Vinicius Oliveira** - Developer Evaluation Submission
