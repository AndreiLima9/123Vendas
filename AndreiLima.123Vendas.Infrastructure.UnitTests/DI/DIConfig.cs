using AndreiLima._123Vendas.Domain.Interfaces.Notifications;
using AndreiLima._123Vendas.Domain.Services.Notifications;
using AndreiLima._123Vendas.Infrastructure.IoC;
using AndreiLima._123Vendas.Mappers;
using Microsoft.Extensions.DependencyInjection;


namespace AndreiLima._123Vendas.Infrastructure.UnitTests.DI
{ 
    public static class DIConfig
    {
        private static ServiceCollection _services;

        public static void AddDependencies()
        {
            if (_services == null)
            {
                _services = new ServiceCollection();
                _services.AddHttpContextAccessor();
                _services.AddAutoMapper(typeof(PurchaseProfile));
                _services.AddScoped<INotification, NotificationService>();
                _services.AddSingleton<IContainer, ServiceProviderProxy>();

                var provider = _services.BuildServiceProvider();
                ServiceLocator.Initialize(provider);
            }

        }
    }
}
