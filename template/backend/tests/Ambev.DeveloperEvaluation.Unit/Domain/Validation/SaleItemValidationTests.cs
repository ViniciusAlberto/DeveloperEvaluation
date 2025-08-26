using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleItemValidationTests
    {
        private readonly Faker faker = new Faker();

        [Fact(DisplayName = "Valid SaleItem should pass validation")]
        public void Given_ValidSaleItem_When_Validated_Then_ShouldNotHaveErrors()
        {
            var product = new ExternalProduct(faker.Random.Guid().ToString(), faker.Commerce.ProductName());
            var item = new SaleItem(product, 5, 100);
            var validator = new SaleItemValidator();

            var result = validator.TestValidate(item);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "SaleItem with invalid data should fail validation")]
        public void Given_InvalidSaleItem_When_Validated_Then_ShouldHaveErrors()
        {
            // Criar ExternalProduct inválido via reflection
            var product = (ExternalProduct)Activator.CreateInstance(typeof(ExternalProduct), true);
            typeof(ExternalProduct).GetProperty("ExternalId")!.SetValue(product, "");
            typeof(ExternalProduct).GetProperty("Name")!.SetValue(product, "");

            // Criar SaleItem inválido via reflection
            var item = (SaleItem)Activator.CreateInstance(typeof(SaleItem), true);
            typeof(SaleItem).GetProperty("Product")!.SetValue(item, product);
            typeof(SaleItem).GetProperty("Quantity")!.SetValue(item, 0);
            typeof(SaleItem).GetProperty("UnitPrice")!.SetValue(item, -10m);
            typeof(SaleItem).GetProperty("Discount")!.SetValue(item, -5m);

            var validator = new SaleItemValidator();
            var result = validator.TestValidate(item);

            result.ShouldHaveValidationErrorFor(i => i.Product.ExternalId);
            result.ShouldHaveValidationErrorFor(i => i.Product.Name);
            result.ShouldHaveValidationErrorFor(i => i.Quantity);
            result.ShouldHaveValidationErrorFor(i => i.UnitPrice);
            result.ShouldHaveValidationErrorFor(i => i.Discount);
        }
    }
}
