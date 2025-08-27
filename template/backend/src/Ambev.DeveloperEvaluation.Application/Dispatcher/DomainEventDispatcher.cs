using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Application.Dispatcher
{
    public interface IDomainEventPublisher
    {
        Task PublishAsync(IDomainEvent domainEvent);
    }
}
