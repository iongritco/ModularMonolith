using MassTransit;
using ToDoApp.EventBus.Interfaces;

namespace ToDoApp.EventBus.MassTransit
{
    public class EventBus : IEventBus
    {
        private readonly IBus _bus;

        public EventBus(IBus bus)
        {
            _bus = bus;
        }

        public async Task Publish<T>(T message)
        {
            if (message is null)
            {
                return;
            }

            await _bus.Publish(message);
        }
    }
}
