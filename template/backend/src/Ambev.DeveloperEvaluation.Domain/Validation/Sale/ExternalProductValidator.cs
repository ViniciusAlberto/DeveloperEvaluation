using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class ExternalProductValidator : AbstractValidator<ExternalProduct>
    {
        public ExternalProductValidator()
        {
            RuleFor(p => p.ExternalId)
                .NotEmpty().WithMessage("Product ExternalId is required.")
                .MaximumLength(100);

            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(200);
        }
    }
}
