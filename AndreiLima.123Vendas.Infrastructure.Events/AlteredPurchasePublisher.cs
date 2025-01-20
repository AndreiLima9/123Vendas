using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;


namespace AndreiLima._123Vendas.Infrastructure.Events
{
    public class AlteredPurchasePublisher : EventPublisherBase<AlteredPurchaseMessage>, IAlteredPurchasePublisher
    {
        public AlteredPurchasePublisher(ILogger<AlteredPurchaseMessage> logger) : base(logger)
        {
        }
    }
}
