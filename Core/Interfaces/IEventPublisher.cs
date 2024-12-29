namespace Core.Interfaces
{
    public interface IEventPublisher<in TMessage> where TMessage : class
    {
        void Publish(TMessage message);
    }
}
