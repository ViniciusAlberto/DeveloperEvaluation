using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
{
    /// <summary>
    /// Provides methods to generate test data for UpdateSaleCommand.
    /// Supports valid, invalid, and edge scenarios.
    /// </summary>
    public static class UpdateSaleCommandTestData
    {
        private static readonly Faker faker = new();

        /// <summary>
        /// Generates a valid UpdateSaleCommand with randomized data.
        /// </summary>
        public static UpdateSaleCommand GenerateValidCommand()
        {
            var command = new UpdateSaleCommand
            {
                SaleId = faker.Random.Guid(),
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
                        UnitPrice = faker.Random.Decimal(10, 100),
                        Discount = faker.Random.Decimal(0, 5)
                    }
                }
            };

            // Calcula o total dos itens
            foreach (var item in command.Items)
                item.Total = (item.UnitPrice * item.Quantity) - item.Discount;

            return command;
        }

        /// <summary>
        /// Generates an invalid UpdateSaleCommand with missing/invalid data.
        /// </summary>
        public static UpdateSaleCommand GenerateInvalidCommand()
        {
            return new UpdateSaleCommand
            {
                SaleId = Guid.Empty,
                Date = DateTime.UtcNow.AddDays(1), // Data futura inválida
                Customer = new CustomerDto { ExternalId = string.Empty, Name = string.Empty },
                Branch = new BranchDto { ExternalId = string.Empty, Name = string.Empty },
                Items = new List<SaleItemDto>() // Nenhum item
            };
        }

        /// <summary>
        /// Generates an UpdateSaleCommand with a product that simulates "not found".
        /// </summary>
        public static UpdateSaleCommand GenerateCommandWithMissingProduct()
        {
            var command = GenerateValidCommand();
            command.Items[0].ProductExternalId = "MISSING_PRODUCT";
            return command;
        }
    }
}
