using AndreiLima._123Vendas.Domain.Entities.Notifications;
using AndreiLima._123Vendas.Domain.Interfaces.Notifications;

namespace AndreiLima._123Vendas.Domain.Services.Notifications
{
    
    public class NotificationService : INotification
    {
        public NotificationService()
        {
            Errors = new List<NotificationError>();
        }

        public IList<NotificationError> Errors { get; set; }
        public bool HasNotification { get => Errors.Any(); }

        public void AddError(string context, string message)
        {
            Errors.Add(new NotificationError { Context = context, Message = message });
        }
    }
}
