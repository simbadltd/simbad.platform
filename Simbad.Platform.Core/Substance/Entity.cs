using System.Collections.Generic;
using Simbad.Platform.Core.Events;
using Simbad.Platform.Core.Substance.IdGeneration;

namespace Simbad.Platform.Core.Substance
{
    public abstract class Entity<TId> : IEventsHolder<TId>
    {
        private readonly List<IEvent> _events;

        public TId Id { get; set; }

        protected Entity()
        {
            Id = IdGenerator.NewId<TId>();
            _events = new List<IEvent>();
        }

        public void Publish(IEvent @event)
        {
            _events.Add(@event);
        }

        public override string ToString()
        {
            return string.Concat("[", GetType().Name, ", ", nameof(Id), " = ", Id, "]");
        }

        public ICollection<IEvent> ExtractEvents()
        {
            var result = new List<IEvent>(_events);
            _events.Clear();
            return result;
        }
    }
}