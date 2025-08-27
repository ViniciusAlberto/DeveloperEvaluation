
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{

    namespace Ambev.DeveloperEvaluation.Unit.Domain.Sales.TestData
    {
        public static class CancelSaleCommandTestData
        {
            private static readonly Faker faker = new Faker();

            /// <summary>
            /// Gera um comando válido para deletar sale
            /// </summary>
            public static CancelSaleCommand GenerateValidCommand()
            {
                return new CancelSaleCommand(Guid.NewGuid());

            }

            /// <summary>
            /// Gera um comando com ID inválido (Guid.Empty)
            /// </summary>
            public static CancelSaleCommand GenerateInvalidCommand()
            {
                return new CancelSaleCommand(Guid.Empty);
            }
        }
    }
}

