namespace AndreiLima._123Vendas.Domain.Interfaces.Notifications
{
    public interface IContainer
    {
        T GetService<T>(Type type);
    }
}
