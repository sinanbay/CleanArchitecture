using System;
namespace Infrastructure.Messaging
{
	public class RabbitMqSettingsDTO
    {
        public string Uri { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string QueueName { get; set; } = null!;
    }
}

