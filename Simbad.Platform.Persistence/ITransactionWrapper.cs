using System;
using System.Collections.Generic;
using System.Data;

namespace Simbad.Platform.Persistence
{
    public interface ITransactionWrapper
    {
        T Fetch<T, TId>(TId id) where T : Dao<TId>;

        Dao<TId> Fetch<TId>(TId id, Type type);

        ICollection<T> Fetch<T, TId>(Func<T, bool> predicate) where T : Dao<TId>;

        ICollection<T> FetchAll<T, TId>(IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        ICollection<Dao<TId>> FetchAll<TId>(Type type);

        void Save<TId>(Dao<TId> model, Type type);

        void Delete<T, TId>(TId id) where T : Dao<TId>;

        void Delete<TId>(TId id, Type type);

        void DeleteAll(Type type);
    }
}