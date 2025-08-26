using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;
using FluentValidation.TestHelper;

namespace Ambev.DeveloperEvaluation.Unit.ValueObjects
{
    public class ExternalProductTests
    {
        [Fact(DisplayName = "Valid ExternalProduct should pass validation")]
        public void ValidExternalProduct_ShouldPassValidation()
        {
            var product = new ExternalProduct("P001", "Product Name");
            var validator = new ExternalProductValidator();

            var result = validator.TestValidate(product);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Invalid ExternalProduct should fail validation")]
        public void InvalidExternalProduct_ShouldFailValidation()
        {
            var product = (ExternalProduct)Activator.CreateInstance(typeof(ExternalProduct), true);
            typeof(ExternalProduct).GetProperty("ExternalId")!.SetValue(product, "");
            typeof(ExternalProduct).GetProperty("Name")!.SetValue(product, "");

            var validator = new ExternalProductValidator();
            var result = validator.TestValidate(product);

            result.ShouldHaveValidationErrorFor(p => p.ExternalId);
            result.ShouldHaveValidationErrorFor(p => p.Name);
        }
    }
}
