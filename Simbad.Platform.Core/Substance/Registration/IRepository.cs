using System;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Substance.Registration
{
    public interface IRepository<TEntity, in TId> where TEntity : Entity<TId>
    {
        TEntity Get(TId id);

        IReadOnlyCollection<TEntity> GetAll();

        void Delete(TId id);

        void DeleteAll();

        void Save(TEntity aggregate);

        TEntity FindSingle(Func<TEntity, bool> predicate);

        ICollection<TEntity> FindAll(Func<TEntity, bool> predicate);

        TEntity Clone(TEntity aggregate);
    }
}