using AndreiLima._123Vendas.Domain.Entities.EventMessages;
using AndreiLima._123Vendas.Domain.Interfaces.Events;
using Microsoft.Extensions.Logging;

namespace AndreiLima._123Vendas.Infrastructure.Events
{
    public class EventPublisherBase<T> : IEventPublisherBase<T> where T : EventMessageBase
    {
        private readonly ILogger<T> _logger;

        public EventPublisherBase(ILogger<T> logger)
        {
            _logger = logger;
        }

        public Task PublishAsync(T message)
        {
            _logger.Log(LogLevel.Information, typeof(T).Name, message);

            return Task.CompletedTask;
        }
    }
}
