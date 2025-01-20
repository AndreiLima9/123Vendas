using AndreiLima._123Vendas.Domain.Entities.EventMessages;

namespace AndreiLima._123Vendas.Domain.Interfaces.Events
{
    public interface IAlteredPurchasePublisher : IEventPublisherBase<AlteredPurchaseMessage>
    {
    }
}
