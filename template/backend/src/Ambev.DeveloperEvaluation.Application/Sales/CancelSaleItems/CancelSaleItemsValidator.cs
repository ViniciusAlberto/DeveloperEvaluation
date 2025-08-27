using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItems
{
    /// <summary>
    /// Validator for CancelSaleItemsCommand
    /// </summary>
    public class CancelSaleItemsCommandValidator : AbstractValidator<CancelSaleItemsCommand>
    {
        /// <summary>
        /// Initializes validation rules for CancelSaleItemsCommand
        /// </summary>
        public CancelSaleItemsCommandValidator()
        {
            RuleForEach(x => x.SaleItemsId).ChildRules(items =>
            {
                items.RuleFor(i => i).NotEmpty().WithMessage("Item Sale ID is required");
            });
        }
    }
}