using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Validator for CanceçSaleCommand
    /// </summary>
    public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetSaleCommand
        /// </summary>
        public CancelSaleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty()
                .WithMessage("Sale ID is required");
           
        }
    }
}
