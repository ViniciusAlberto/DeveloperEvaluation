using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
{
    /// <summary>
    /// Provides methods to generate test data for CreateSaleCommand.
    /// Supports both valid and invalid scenarios.
    /// </summary>
    public static class CreateSaleCommandTestData
    {
        private static readonly Faker faker = new Faker();

        /// <summary>
        /// Generates a valid CreateSaleCommand with randomized customer, branch, and items.
        /// </summary>
        public static CreateSaleCommand GenerateValidCommand()
        {
            var command = new CreateSaleCommand
            {
                SaleNumber = faker.Random.AlphaNumeric(10),
                Date = DateTime.UtcNow,
                Customer = new CustomerDto
                {
                    ExternalId = faker.Random.Guid().ToString(),
                    Name = faker.Person.FullName
                },
                Branch = new BranchDto
                {
                    ExternalId = faker.Random.Guid().ToString(),
                    Name = faker.Company.CompanyName()
                },
                Items = new List<SaleItemDto>
                {
                    new SaleItemDto
                    {
                        ProductExternalId = faker.Random.Guid().ToString(),
                        ProductName = faker.Commerce.ProductName(),
                        Quantity = faker.Random.Int(1, 10),
                        UnitPrice = faker.Random.Decimal(1, 100),
                        Discount = faker.Random.Decimal(0, 10)
                    }
                }
            };

            // Calcula o Total dos itens
            foreach (var item in command.Items)
            {
                item.Total = (item.UnitPrice * item.Quantity) - item.Discount;
            }

            return command;
        }

        /// <summary>
        /// Generates a CreateSaleCommand with invalid fields for testing validation errors.
        /// </summary>
        public static CreateSaleCommand GenerateInvalidCommand()
        {
            return new CreateSaleCommand
            {
                SaleNumber = string.Empty, // inválido
                Date = DateTime.UtcNow.AddDays(1), // futura
                Customer = new CustomerDto
                {
                    ExternalId = string.Empty,
                    Name = string.Empty
                },
                Branch = new BranchDto
                {
                    ExternalId = string.Empty,
                    Name = string.Empty
                },
                Items = new List<SaleItemDto>() // vazio
            };
        }

        /// <summary>
        /// Generates a CreateSaleCommand with a product that simulates "not found" scenario.
        /// </summary>
        public static CreateSaleCommand GenerateCommandWithMissingProduct()
        {
            var command = GenerateValidCommand();
            command.Items[0].ProductExternalId = "MISSING_PRODUCT";
            return command;
        }
    }
}
