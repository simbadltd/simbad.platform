using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Contracts;
using Simbad.Platform.Persistence;

namespace Simbad.Platform.Persistence
{
    // todo[kk]: think about removal of this class
    public sealed class TransactionWrapper : ITransactionWrapper
    {
        private readonly IDbTransaction _transaction;

        private readonly IDbConnection _connection;

        private readonly IStorageAdapter _storageAdapter;

        public TransactionWrapper(IDbConnection connection, IDbTransaction transaction, IStorageAdapter storageAdapter)
        {
            _connection = connection;
            _transaction = transaction;
            _storageAdapter = storageAdapter;
        }

        public T Fetch<T, TId>(TId id) where T : Dao<TId>
        {
            return _storageAdapter.Fetch<T, TId>(id, _connection, _transaction);
        }

        public Dao<TId> Fetch<TId>(TId id, Type type)
        {
            return _storageAdapter.Fetch(id, type, _connection, _transaction);
        }

        public ICollection<T> Fetch<T, TId>(Func<T, bool> predicate) where T : Dao<TId>
        {
            return _storageAdapter.Fetch<T, TId>(predicate, _connection, _transaction);
        }

        public ICollection<T> FetchAll<T, TId>(IDbConnection connection, IDbTransaction transaction) where T : Dao<TId>
        {
            return _storageAdapter.FetchAll<T, TId>(_connection, _transaction);
        }

        public ICollection<Dao<TId>> FetchAll<TId>(Type type)
        {
            return _storageAdapter.FetchAll<TId>(type, _connection, _transaction);
        }

        public void Save<TId>(Dao<TId> model, Type type)
        {
            _storageAdapter.Save(model, type, _connection, _transaction);
        }

        public void Delete<T, TId>(TId id) where T : Dao<TId>
        {
            Delete(id, typeof(T));
        }

        public void Delete<TId>(TId id, Type type)
        {
            _storageAdapter.Delete(id, type, _connection, _transaction);
        }

        public void DeleteAll(Type type)
        {
            _storageAdapter.DeleteAll(type, _connection, _transaction);
        }
    }
}
