using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
{
    public static class DeleteSaleCommandTestData
    {
        private static readonly Faker faker = new Faker();

        /// <summary>
        /// Gera um comando válido para deletar sale
        /// </summary>
        public static DeleteSaleCommand GenerateValidCommand()
        {
            return new DeleteSaleCommand(Guid.NewGuid());
            
        }

        /// <summary>
        /// Gera um comando com ID inválido (Guid.Empty)
        /// </summary>
        public static DeleteSaleCommand GenerateInvalidCommand()
        {
            return new DeleteSaleCommand(Guid.Empty);            
        }
    }
}
