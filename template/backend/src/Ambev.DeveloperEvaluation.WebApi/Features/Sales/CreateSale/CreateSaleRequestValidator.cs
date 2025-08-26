using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator : AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().WithMessage("CustomerId é obrigatório.");
            RuleFor(x => x.BranchId).NotEmpty().WithMessage("BranchId é obrigatório.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Data é obrigatória.");
            RuleFor(x => x.Items).NotEmpty().WithMessage("A venda deve conter ao menos um item.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty().WithMessage("ProductId é obrigatório.");
                items.RuleFor(i => i.ProductName).NotEmpty().WithMessage("Nome do produto é obrigatório.");
                items.RuleFor(i => i.Quantity).GreaterThan(0).WithMessage("Quantidade deve ser maior que zero.");
                items.RuleFor(i => i.UnitPrice).GreaterThan(0).WithMessage("Preço unitário deve ser maior que zero.");
            });
        }
    }
}
