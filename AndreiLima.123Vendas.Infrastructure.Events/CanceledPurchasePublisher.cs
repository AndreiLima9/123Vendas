using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;


namespace AndreiLima._123Vendas.Infrastructure.Events
{
    public class CanceledPurchasePublisher : EventPublisherBase<CanceledPurchaseMessage>, ICanceledPurchasePublisher
    {
        public CanceledPurchasePublisher(ILogger<CanceledPurchaseMessage> logger) : base(logger)
        {
        }
    }
}
