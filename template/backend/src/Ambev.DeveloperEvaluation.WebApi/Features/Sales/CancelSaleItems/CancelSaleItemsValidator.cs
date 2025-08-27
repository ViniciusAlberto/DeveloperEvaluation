using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleItems
{
    /// <summary>
    /// Validator for CancelSaleItemsRequest
    /// </summary>
    public class CancelSaleItemsRequestValidator : AbstractValidator<CancelSaleItemsRequest>
    {
        /// <summary>
        /// Initializes validation rules for CancelSaleItemsRequest
        /// </summary>
        public CancelSaleItemsRequestValidator()
        {
            RuleForEach(x => x.SaleItemsIds).ChildRules(items =>
            {
                items.RuleFor(i => i).NotEmpty().WithMessage("Item Sale ID is required");
            });
        }
    }
}