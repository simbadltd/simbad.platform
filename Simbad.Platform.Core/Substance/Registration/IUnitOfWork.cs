using System;
using System.Collections.Generic;
using Simbad.Platform.Core.Events;

namespace Simbad.Platform.Core.Substance.Registration
{
    public interface IUnitOfWork<in TId>
    {
        void Commit();
        
        void TrackEvents(ICollection<IEvent> events);
        
        void Save(object model, Type type);
        
        void Delete(TId id, Type type);
        
        void DeleteAll(Type type);
    }
}