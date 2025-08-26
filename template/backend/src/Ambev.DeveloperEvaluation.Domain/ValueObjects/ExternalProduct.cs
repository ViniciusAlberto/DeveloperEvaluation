namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ExternalProduct
    {
        public string ExternalId { get; private set; }
        public string Name { get; private set; }
              

        protected ExternalProduct() { }

        public ExternalProduct(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }
    }
}
