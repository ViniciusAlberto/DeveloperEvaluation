using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class CancelSaleItemsCommandTestData
    {
        private static readonly Faker _faker = new Faker();

        /// <summary>
        /// Gera um comando válido para cancelar itens de uma venda
        /// </summary>
        public static CancelSaleItemsCommand GenerateValidCommand()
        {
            var saleId = Guid.NewGuid();
            var itemIds = new List<Guid>
            {
                Guid.NewGuid(),
                Guid.NewGuid()
            };

            return new CancelSaleItemsCommand(saleId, itemIds);
        }

        /// <summary>
        /// Gera um comando inválido (venda vazia ou sem itens)
        /// </summary>
        public static CancelSaleItemsCommand GenerateInvalidCommand()
        {
            return new CancelSaleItemsCommand(Guid.Empty, new List<Guid>());
        }

        /// <summary>
        /// Gera um comando com itens inexistentes
        /// </summary>
        public static CancelSaleItemsCommand GenerateNonExistingItemsCommand()
        {
            var saleId = Guid.NewGuid();
            var itemIds = new List<Guid> { Guid.NewGuid(), Guid.NewGuid() };

            return new CancelSaleItemsCommand(saleId, itemIds);
        }
    }
}
