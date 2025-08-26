using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Domain.Events;
using Rebus.Bus;

namespace Ambev.DeveloperEvaluation.Rebus
{
    public class RebusEventPublisher : IDomainEventPublisher
    {
        private readonly IBus _bus;

        public RebusEventPublisher(IBus bus)
        {
            _bus = bus;
        }

        public async Task PublishAsync(IDomainEvent domainEvent)
        {
            // Publica o evento no barramento Rebus
        }
    }
}
