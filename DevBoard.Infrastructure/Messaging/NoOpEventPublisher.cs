using DevBoard.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevBoard.Infrastructure.Messaging
{
    public class NoOpEventPublisher : IEventPublisher
    {
        public Task PublishAsync<T>(string routingKey, T message)
        {
            // temporary no-op until RabbitMQ is implemented in Phase H
            return Task.CompletedTask;
        }
    }
}
