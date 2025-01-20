namespace AndreiLima._123Vendas.Domain.Services.Notifications
{
    public static class ServiceLocator
    {
        public static IServiceProvider Provider { get; private set; }

        public static void Initialize(IServiceProvider provider)
        {
            Provider = provider;
        }
    }
}
