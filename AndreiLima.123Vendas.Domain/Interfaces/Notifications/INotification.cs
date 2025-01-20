using AndreiLima._123Vendas.Domain.Entities.Notifications;


namespace AndreiLima._123Vendas.Domain.Interfaces.Notifications
{
    public interface INotification
    {
        public IList<NotificationError> Errors { get; set; }
        public bool HasNotification { get; }
        void AddError(string context, string message);
    }
}
