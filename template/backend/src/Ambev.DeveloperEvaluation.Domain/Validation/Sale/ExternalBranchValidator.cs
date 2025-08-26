using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation.Sale
{
    public class ExternalBranchValidator : AbstractValidator<ExternalBranch>
    {
        public ExternalBranchValidator()
        {
            RuleFor(b => b.ExternalId)
                .NotEmpty().WithMessage("Branch ExternalId is required.")
                .MaximumLength(100);

            RuleFor(b => b.Name)
                .NotEmpty().WithMessage("Branch Name is required.")
                .MaximumLength(200);
        }
    }
}
