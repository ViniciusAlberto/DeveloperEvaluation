using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSale
{
    /// <summary>
    /// Validator for ListSaleRequest
    /// </summary>
    public class ListSaleRequestValidator : AbstractValidator<ListSaleRequest>
    {
        /// <summary>
        /// Initializes validation rules for ListSaleRequest
        /// </summary>
        public ListSaleRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page number must be greater than 0");

            RuleFor(x => x.PageSize)
                .GreaterThan(0)
                .LessThanOrEqualTo(100)
                .WithMessage("Page size must be between 1 and 100");

            RuleFor(x => x.OrderBy)
                .NotEmpty()
                .WithMessage("OrderBy is required")
                .Must(BeAValidOrderBy)
                .WithMessage("OrderBy format is invalid. Example: 'id asc' or 'date desc'");
        }

        private bool BeAValidOrderBy(string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy))
                return false;

            // formato esperado: "campo asc|desc"
            var parts = orderBy.Trim().Split(' ');
            if (parts.Length != 2)
                return false;

            var direction = parts[1].ToLower();
            return direction == "asc" || direction == "desc";
        }
    }
}
