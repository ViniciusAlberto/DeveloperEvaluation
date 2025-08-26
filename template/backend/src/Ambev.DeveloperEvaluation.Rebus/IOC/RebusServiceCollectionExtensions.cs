using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Domain.Events;
using Microsoft.Extensions.DependencyInjection;
using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;

namespace Ambev.DeveloperEvaluation.Rebus.IOC
{
    public static class RebusServiceCollectionExtensions
    {
        public static IServiceCollection AddRebusServices(this IServiceCollection services)
        {
            // Registrar o IBus com transporte InMemory (exemplo)
            services.AddRebus(configure => configure
                .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "RebusQueue"))
                .Routing(r => r.TypeBased().MapAssemblyOf<SaleCreatedEvent>("RebusQueue"))
            );

            // Registrar o DomainEventPublisher que depende do IBus
            services.AddTransient<IDomainEventPublisher, RebusEventPublisher>();

            return services;
        }
    }
}
