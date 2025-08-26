using Ambev.DeveloperEvaluation.Domain.Events;
namespace Ambev.DeveloperEvaluation.Application.Dispatcher
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
