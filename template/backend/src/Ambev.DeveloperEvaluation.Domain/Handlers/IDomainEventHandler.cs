using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Handlers
{
    public interface IDomainEventHandler<T> where T : IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
