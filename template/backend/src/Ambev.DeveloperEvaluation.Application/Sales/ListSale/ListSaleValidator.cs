using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.ListSale
{
    public class ListSaleCommandValidator : AbstractValidator<ListSaleCommand>
    {
        public ListSaleCommandValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0).LessThanOrEqualTo(100)
                .WithMessage("PageSize must be between 1 and 100");

            RuleFor(x => x.OrderBy)
                .NotEmpty().WithMessage("OrderBy is required");
        }
    }
}
