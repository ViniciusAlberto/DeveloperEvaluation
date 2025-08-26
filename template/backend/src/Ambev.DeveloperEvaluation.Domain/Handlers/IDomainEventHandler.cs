namespace Ambev.DeveloperEvaluation.Domain.Handlers
{
    public interface IDomainEventHandler<T> where T : Events.IDomainEvent
    {
        void Handle(T domainEvent);
    }
}
