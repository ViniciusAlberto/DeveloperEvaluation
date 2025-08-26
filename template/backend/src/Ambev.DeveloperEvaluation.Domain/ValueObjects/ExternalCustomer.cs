namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ExternalCustomer
    {
        public string ExternalId { get; private set; }
        public string Name { get; private set; }

        private ExternalCustomer() { }

        public ExternalCustomer(string externalId, string name)
        {
            ExternalId = externalId;
            Name = name;
        }
    }
}
