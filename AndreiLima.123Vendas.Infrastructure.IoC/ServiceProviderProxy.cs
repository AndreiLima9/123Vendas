using AndreiLima._123Vendas.Domain.Interfaces.Notifications;
using Microsoft.AspNetCore.Http;

namespace AndreiLima._123Vendas.Infrastructure.IoC
{
    public class ServiceProviderProxy : IContainer
    {
        private readonly IHttpContextAccessor _contextAcessor;

        public ServiceProviderProxy(IHttpContextAccessor contextAcessor)
        {
            _contextAcessor = contextAcessor;
        }
        public T GetService<T>(Type type)
        {
            return (T)_contextAcessor.HttpContext.RequestServices.GetService(type);
        }
    }
}
