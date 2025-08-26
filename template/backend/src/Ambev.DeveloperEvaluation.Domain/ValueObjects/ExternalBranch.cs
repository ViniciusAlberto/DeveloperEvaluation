namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ExternalBranch
    {
        public string ExternalId { get; private set; }   // Id da filial no domínio de Branches
        public string Name { get; private set; }         // Nome da filial (denormalizado)

        protected ExternalBranch() { } // Necessário para EF Core

        public ExternalBranch(string externalId, string name)
        {
            if (string.IsNullOrWhiteSpace(externalId))
                throw new ArgumentException("ExternalId da filial é obrigatório.", nameof(externalId));

            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Nome da filial é obrigatório.", nameof(name));

            ExternalId = externalId;
            Name = name;
        }

        // Value Object: igualdade por valores
        public override bool Equals(object? obj)
        {
            if (obj is not ExternalBranch other) return false;
            return ExternalId == other.ExternalId && Name == other.Name;
        }

        public override int GetHashCode() => HashCode.Combine(ExternalId, Name);
    }
}
