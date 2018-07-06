namespace Simbad.Platform.Core.Events
{
    public interface IEventDispatcher
    {
        void Dispatch(IEvent @event);
    }
}