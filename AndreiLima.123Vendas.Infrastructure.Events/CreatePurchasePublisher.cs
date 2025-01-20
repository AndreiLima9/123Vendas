using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;


namespace AndreiLima._123Vendas.Infrastructure.Events
{
    public class CreatePurchasePublisher : EventPublisherBase<CreatePurchaseMessage>, ICreatePurchasePublisher
    { 
        public CreatePurchasePublisher(ILogger<CreatePurchaseMessage> logger) : base(logger)
        {
        }
    }
}
