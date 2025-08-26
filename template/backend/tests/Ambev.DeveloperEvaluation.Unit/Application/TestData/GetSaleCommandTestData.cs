using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
{
    /// <summary>
    /// Provides methods to generate test data for GetSaleCommand.
    /// Supports both valid and invalid scenarios.
    /// </summary>
    public static class GetSaleCommandTestData
    {
        private static readonly Faker faker = new();

        /// <summary>
        /// Generates a valid GetSaleCommand with a random Guid.
        /// </summary>
        public static GetSaleCommand GenerateValidCommand()
        {
            return new GetSaleCommand(faker.Random.Guid());            
        }

        /// <summary>
        /// Generates an invalid GetSaleCommand with an empty Guid.
        /// </summary>
        public static GetSaleCommand GenerateInvalidCommand()
        {
            return new GetSaleCommand(Guid.Empty);            
        }
    }
}
