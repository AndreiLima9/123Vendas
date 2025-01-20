using AndreiLima._123Vendas.Domain.Entities.EventMessages;

namespace AndreiLima._123Vendas.Domain.Interfaces.Events
{
    public interface ICanceledPurchasePublisher : IEventPublisherBase<CanceledPurchaseMessage>
    {
    }
}
