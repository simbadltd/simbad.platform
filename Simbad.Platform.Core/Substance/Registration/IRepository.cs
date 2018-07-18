using System;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Substance.Registration
{
    public interface IRepository<TBusinessObject> where TBusinessObject : BusinessObject
    {
        TBusinessObject Get(Guid id);

        IReadOnlyCollection<TBusinessObject> GetAll();

        void Delete(Guid id);

        void DeleteAll();

        void Save(TBusinessObject businessObject);

        TBusinessObject FindSingle(Func<TBusinessObject, bool> predicate);

        ICollection<TBusinessObject> FindAll(Func<TBusinessObject, bool> predicate);

        TBusinessObject Clone(TBusinessObject source);
    }
}