using Ambev.DeveloperEvaluation.Application.Sales.ListSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
{
    /// <summary>
    /// Provides methods to generate test data for ListSaleCommand.
    /// Supports both valid and invalid scenarios.
    /// </summary>
    public static class ListSaleCommandTestData
    {
        private static readonly Faker faker = new();

        /// <summary>
        /// Generates a valid ListSaleCommand with random page and pageSize.
        /// </summary>
        public static ListSaleCommand GenerateValidCommand()
        {
            return new ListSaleCommand
            {
                Page = faker.Random.Int(1, 5),
                PageSize = faker.Random.Int(1, 20),
                OrderBy = "id asc"
            };
        }

        /// <summary>
        /// Generates an invalid ListSaleCommand (page or pageSize zero).
        /// </summary>
        public static ListSaleCommand GenerateInvalidCommand()
        {
            return new ListSaleCommand
            {
                Page = 0,
                PageSize = 0,
                OrderBy = ""
            };
        }
    }
}
