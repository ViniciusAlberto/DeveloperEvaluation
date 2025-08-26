using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class ExternalCustomerValidator : AbstractValidator<ExternalCustomer>
    {
        public ExternalCustomerValidator()
        {
            RuleFor(c => c.ExternalId)
                .NotEmpty().WithMessage("Customer ExternalId is required.")
                .MaximumLength(100);

            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Customer Name is required.")
                .MaximumLength(200);
        }
    }
}
