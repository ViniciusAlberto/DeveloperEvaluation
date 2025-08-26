using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class SaleValidator : AbstractValidator<Entities.Sale>
    {
        public SaleValidator()
        {
            RuleFor(s => s.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber is required.")
                .MaximumLength(50);

            RuleFor(s => s.Date)
                .LessThanOrEqualTo(DateTime.UtcNow)
                .WithMessage("Sale date cannot be in the future.");

            RuleFor(s => s.Customer)
                .SetValidator(new ExternalCustomerValidator());

            RuleFor(s => s.Branch)
                .SetValidator(new ExternalBranchValidator());

            RuleForEach(s => s.Items)
                .SetValidator(new SaleItemValidator());

            RuleFor(s => s.TotalAmount)
                .GreaterThanOrEqualTo(0).WithMessage("TotalAmount must be greater than or equal to 0.");
        }
    }
}
