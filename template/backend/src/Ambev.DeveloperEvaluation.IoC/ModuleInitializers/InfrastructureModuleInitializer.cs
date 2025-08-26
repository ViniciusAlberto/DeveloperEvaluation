using Ambev.DeveloperEvaluation.Application.Dispatcher;
using Ambev.DeveloperEvaluation.Clients;
using Ambev.DeveloperEvaluation.Domain.Clients;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.ORM;
using Ambev.DeveloperEvaluation.ORM.Repositories;
using Ambev.DeveloperEvaluation.Rebus;
using Ambev.DeveloperEvaluation.Rebus.IOC;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Ambev.DeveloperEvaluation.IoC.ModuleInitializers;

public class InfrastructureModuleInitializer : IModuleInitializer
{
    public void Initialize(WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<DbContext>(provider => provider.GetRequiredService<DefaultContext>());
        builder.Services.AddScoped<IUserRepository, UserRepository>();
        builder.Services.AddScoped<ISaleRepository, SaleRepository>();

        builder.Services.AddHttpClient<IProductClient, ProductClient>(client =>
        {
            // replace with actual base address
            client.BaseAddress = new Uri("https://api.example.com"); 
                                                                     
        });

        builder.Services.AddRebusServices();

    }
}