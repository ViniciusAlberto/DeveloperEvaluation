using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(c => c.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber is required.")
                .MaximumLength(50);

            RuleFor(c => c.Date)
                .LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Sale date cannot be in the future.");

            RuleFor(c => c.Customer).NotNull().SetValidator(new CustomerDtoValidator());
            RuleFor(c => c.Branch).NotNull().SetValidator(new BranchDtoValidator());
            RuleFor(c => c.Items).NotEmpty().WithMessage("Sale must have at least one item.");
            RuleForEach(c => c.Items).SetValidator(new SaleItemDtoValidator());
        }
    }

    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {
        public CustomerDtoValidator()
        {
            RuleFor(c => c.ExternalId).NotEmpty().WithMessage("Customer ExternalId is required.");
            RuleFor(c => c.Name).NotEmpty().WithMessage("Customer Name is required.");
        }
    }

    public class BranchDtoValidator : AbstractValidator<BranchDto>
    {
        public BranchDtoValidator()
        {
            RuleFor(b => b.ExternalId).NotEmpty().WithMessage("Branch ExternalId is required.");
            RuleFor(b => b.Name).NotEmpty().WithMessage("Branch Name is required.");
        }
    }

    public class SaleItemDtoValidator : AbstractValidator<SaleItemDto>
    {
        public SaleItemDtoValidator()
        {
            RuleFor(i => i.ProductExternalId).NotEmpty().WithMessage("ProductExternalId is required.");
            RuleFor(i => i.ProductName).NotEmpty().WithMessage("ProductName is required.");
            RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantity must be greater than 0.");
            RuleFor(i => i.UnitPrice).GreaterThanOrEqualTo(0).WithMessage("UnitPrice must be >= 0.");
            RuleFor(i => i.Discount).GreaterThanOrEqualTo(0).WithMessage("Discount cannot be negative.");
            RuleFor(i => i.Total).GreaterThanOrEqualTo(0).WithMessage("Total must be >= 0.");
        }
    }
}
