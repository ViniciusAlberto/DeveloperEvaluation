using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class SaleItemValidator : AbstractValidator<SaleItem>
    {
        public SaleItemValidator()
        {
            RuleFor(i => i.Product)
                .SetValidator(new ExternalProductValidator());

            RuleFor(i => i.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than 0.");

            RuleFor(i => i.UnitPrice)
                .GreaterThanOrEqualTo(0).WithMessage("UnitPrice must be greater than or equal to 0.");

            RuleFor(i => i.Discount)
                .GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");

            RuleFor(i => i.Total)
                .GreaterThanOrEqualTo(0).WithMessage("Total must be greater than or equal to 0.");
        }
    }
}
