using AndreiLima._123Vendas.Domain.Entities;
using System.Reflection;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using AndreiLima._123Vendas.Domain.Interfaces.Notifications;
using AndreiLima._123Vendas.Domain.Services.Notifications;
using AndreiLima._123Vendas.Infrastructure.Data.Repository;
using Microsoft.Extensions.DependencyInjection;
using AndreiLima._123Vendas.Infrastructure.Events;
using AndreiLima._123Vendas.Domain.Interfaces.Services;
using AndreiLima._123Vendas.Domain.Services;


namespace AndreiLima._123Vendas.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static void AddDependencyInjection(this IServiceCollection service)
        {
            //Notification
            service.AddScoped<INotification, NotificationService>();
            service.AddScoped<ICreatePurchasePublisher, CreatePurchasePublisher>();
            service.AddScoped<IAlteredPurchasePublisher, AlteredPurchasePublisher>();
            service.AddScoped<ICanceledPurchasePublisher, CanceledPurchasePublisher>();
            service.AddScoped<IPurchaseService, PurchaseService>();
            service.AddSingleton<IServiceProvider, ServiceProvider>();

            //Dependency Injection of Services and Repositories
            service.AddDependencyByName(typeof(IEventPublisherBase<>).Assembly, "Publisher");
            service.AddDependencyByName(typeof(RepositoryContext).Assembly, "Repository");
            service.AddDependencyByName(typeof(EntityBase).Assembly, "Service");
        }

        private static void AddDependencyByName(this IServiceCollection service, Assembly assembly, string endFileName)
        {
            var services = assembly.GetTypes().Where(type =>
            type.GetTypeInfo().IsClass && type.Name.EndsWith(endFileName) &&
            !type.GetTypeInfo().IsAbstract);

            foreach (var serviceType in services)
            {
                var allInterfaces = serviceType.GetInterfaces();
                var mainInterfaces = allInterfaces.Except(allInterfaces.SelectMany(t => t.GetInterfaces()));

                foreach (var iServiceType in mainInterfaces)
                {
                    service.AddScoped(iServiceType, serviceType);
                }
            }
        }
    }
}
