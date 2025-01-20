namespace AndreiLima._123Vendas.Domain.Entities.EventMessages
{
    public class EventMessageBase
    {
        public EventMessageBase()
        {
            EventId = Guid.NewGuid();
        }

        public Guid EventId { get; set; }
    }
}
