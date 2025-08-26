using Ambev.DeveloperEvaluation.Domain.Validation.Sale;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Xunit;
using FluentValidation.TestHelper;

namespace Ambev.DeveloperEvaluation.Unit.ValueObjects
{
    public class ExternalBranchTests
    {
        [Fact(DisplayName = "Valid ExternalBranch should pass validation")]
        public void ValidExternalBranch_ShouldPassValidation()
        {
            var branch = new ExternalBranch("BR001", "Main Branch");
            var validator = new ExternalBranchValidator();

            var result = validator.TestValidate(branch);
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact(DisplayName = "Invalid ExternalBranch should fail validation")]
        public void InvalidExternalBranch_ShouldFailValidation()
        {
            var branch = (ExternalBranch)Activator.CreateInstance(typeof(ExternalBranch), true);
            typeof(ExternalBranch).GetProperty("ExternalId")!.SetValue(branch, "");
            typeof(ExternalBranch).GetProperty("Name")!.SetValue(branch, "");

            var validator = new ExternalBranchValidator();
            var result = validator.TestValidate(branch);

            result.ShouldHaveValidationErrorFor(b => b.ExternalId);
            result.ShouldHaveValidationErrorFor(b => b.Name);
        }
    }
}
