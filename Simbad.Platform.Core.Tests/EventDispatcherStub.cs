using Simbad.Platform.Core.Events;

namespace Simbad.Platform.Core.Tests
{
    public sealed class EventDispatcherStub : IEventDispatcher
    {
        public void Dispatch(IEvent @event)
        {
        }
    }
}