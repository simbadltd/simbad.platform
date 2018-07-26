﻿using System;
using System.Collections.Generic;
using System.Data;
using Simbad.Platform.Persistence.Storage;

namespace Simbad.Platform.Persistence.Transactions
{
    public interface ITransactionWrapper
    {
        T Fetch<T>(Guid id) where T : Dao;

        Dao Fetch(Guid id, Type type);

        ICollection<T> Fetch<T>(Func<T, bool> predicate) where T : Dao;

        ICollection<T> FetchAll<T>(IDbConnection connection, IDbTransaction transaction) where T : Dao;

        ICollection<Dao> FetchAll(Type type);

        void Save(Dao dao, Type type);

        void Delete<T>(Guid id) where T : Dao;

        void Delete(Guid id, Type type);

        void DeleteAll(Type type);
    }
}