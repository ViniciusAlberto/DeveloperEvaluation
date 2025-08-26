using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    public static class SaleTestData
    {
        private static readonly Faker faker = new Faker();

        public static ExternalCustomer GenerateCustomer() =>
            new ExternalCustomer(faker.Random.Guid().ToString(), faker.Person.FullName);

        public static ExternalBranch GenerateBranch() =>
            new ExternalBranch(faker.Random.Guid().ToString(), faker.Company.CompanyName());

        public static ExternalProduct GenerateProduct() =>
            new ExternalProduct(faker.Random.Guid().ToString(), faker.Commerce.ProductName());

        public static Sale GenerateSale()
        {
            var customer = GenerateCustomer();
            var branch = GenerateBranch();
            var sale = new Sale(
                saleNumber: faker.Random.AlphaNumeric(10),
                date: DateTime.Now,
                customer: customer,
                branch: branch
            );

            var product = GenerateProduct();
            sale.AddItem(product, 2, 100);

            return sale;
        }
    }


}
