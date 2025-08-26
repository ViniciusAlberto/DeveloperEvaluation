using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;
using FluentValidation.TestHelper;

namespace Ambev.DeveloperEvaluation.Unit.ValueObjects
{
    public class ExternalCustomerTests
    {
        [Fact(DisplayName = "Valid ExternalCustomer should pass validation")]
        public void ValidExternalCustomer_ShouldPassValidation()
        {
            var customer = new ExternalCustomer("123", "John Doe");
            var validator = new ExternalCustomerValidator();

            var result = validator.TestValidate(customer);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Invalid ExternalCustomer should fail validation")]
        public void InvalidExternalCustomer_ShouldFailValidation()
        {
            var customer = (ExternalCustomer)Activator.CreateInstance(typeof(ExternalCustomer), true);
            typeof(ExternalCustomer).GetProperty("ExternalId")!.SetValue(customer, "");
            typeof(ExternalCustomer).GetProperty("Name")!.SetValue(customer, "");

            var validator = new ExternalCustomerValidator();
            var result = validator.TestValidate(customer);

            result.ShouldHaveValidationErrorFor(c => c.ExternalId);
            result.ShouldHaveValidationErrorFor(c => c.Name);
        }
    }
}
