using System;
using System.Collections.Generic;
using System.Data;

namespace Simbad.Platform.Persistence
{
    public interface IStorageAdapter
    {
        T Fetch<T, TId>(TId id, IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        Dao<TId> Fetch<TId>(TId id, Type type, IDbConnection connection, IDbTransaction transaction);

        ICollection<T> Fetch<T, TId>(Func<T, bool> predicate, IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        ICollection<T> FetchAll<T, TId>(IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        ICollection<Dao<TId>> FetchAll<TId>(Type type, IDbConnection connection, IDbTransaction transaction);

        void Transaction(Action<ITransactionWrapper> action);

        TResult Transaction<TResult>(Func<ITransactionWrapper, TResult> action);

        void Save<T, TId>(T model, IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        void Save<TId>(Dao<TId> model, Type type, IDbConnection connection, IDbTransaction transaction);

        void Delete<T, TId>(TId id, IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>;

        void Delete<TId>(TId id, Type type, IDbConnection connection, IDbTransaction transaction);

        void DeleteAll(Type type, IDbConnection connection, IDbTransaction transaction);
    }
}