using System;
using System.Collections.Generic;
using Simbad.Platform.Core.Events;

namespace Simbad.Platform.Core.Substance.Registration
{
    public interface IUnitOfWork
    {
        void Commit();
        
        void TrackEvents(ICollection<IEvent> events);
        
        void Save(object dao, Type type);
        
        void Delete(Guid id, Type type);
        
        void DeleteAll(Type type);
    }
}