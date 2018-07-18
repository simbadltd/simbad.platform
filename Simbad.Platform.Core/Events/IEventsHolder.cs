using System;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Events
{
    internal interface IEventsHolder
    {
        Guid Id { get; }

        ICollection<IEvent> ExtractEvents();
    }
}