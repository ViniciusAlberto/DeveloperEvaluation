using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using Bogus;
using FluentValidation.TestHelper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Validation
{
    public class SaleValidationTests
    {
        private readonly Faker faker = new Faker();

        [Fact(DisplayName = "Valid Sale should pass validation")]
        public void Given_ValidSale_When_Validated_Then_ShouldNotHaveErrors()
        {         
            var sale = SaleTestData.GenerateSale();           

            var validator = new SaleValidator();
            var result = validator.TestValidate(sale);

            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Sale with invalid data should fail validation")]
        public void Given_InvalidSale_When_Validated_Then_ShouldHaveErrors()
        {
            var sale = SaleTestData.GenerateSale();

            // Tornar inválido para teste
            typeof(Sale).GetProperty("SaleNumber")!.SetValue(sale, "");
            typeof(Sale).GetProperty("Date")!.SetValue(sale, DateTime.UtcNow.AddDays(1));

            var customer = sale.Customer;
            typeof(ExternalCustomer).GetProperty("ExternalId")!.SetValue(customer, "");
            typeof(ExternalCustomer).GetProperty("Name")!.SetValue(customer, "");

            // Validator
            var validator = new SaleValidator();
            var result = validator.TestValidate(sale);

            // Assert
            result.ShouldHaveValidationErrorFor(s => s.SaleNumber);
            result.ShouldHaveValidationErrorFor(s => s.Date);
            result.ShouldHaveValidationErrorFor(s => s.Customer.ExternalId);
            result.ShouldHaveValidationErrorFor(s => s.Customer.Name);
        }
    }
}