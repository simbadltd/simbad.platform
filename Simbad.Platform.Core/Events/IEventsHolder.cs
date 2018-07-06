using System.Collections.Generic;

namespace Simbad.Platform.Core.Events
{
    internal interface IEventsHolder<out TId>
    {
        TId Id { get; }

        ICollection<IEvent> ExtractEvents();
    }
}