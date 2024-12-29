using Core.Interfaces;
using MassTransit;

namespace Infrastructure.Messaging
{
    public class EventPublisher<TMessage> : IEventPublisher<TMessage> where TMessage : class
    {
        private readonly IPublishEndpoint publishEndpoint;

        public EventPublisher(IPublishEndpoint publishEndpoint)
		{
            this.publishEndpoint = publishEndpoint;
        }

        public async void Publish(TMessage message)
        {
            await publishEndpoint.Publish(message);
        }
    }
}

