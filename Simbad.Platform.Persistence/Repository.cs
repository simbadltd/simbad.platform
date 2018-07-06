using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Simbad.Platform.Core;
using Simbad.Platform.Core.Substance;
using Simbad.Platform.Core.Substance.IdGeneration;
using Simbad.Platform.Core.Substance.Registration;

namespace Simbad.Platform.Persistence
{
    public sealed class Repository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : Entity<TId>
    {
        private const string Dao2EntityMethodName = "Dao2Entity";
        
        private const string Entity2DaoMethodName = "Entity2Dao";

        private static Type DaoType => EntityMapping.DaoTypeFor(typeof(TEntity));

        private readonly object _syncRoot = new object();

        private readonly Dictionary<TId, TEntity> _cache = new Dictionary<TId, TEntity>();

        private readonly IStorageAdapter _storageAdapter;

        private readonly IUnitOfWork<TId> _unitOfWork;

        private bool _allFetched;

        public Repository(IUnitOfWork<TId> unitOfWork)
        {
            _storageAdapter = GlobalConfigurationExtension.ResolveStorageAdapter();
            _unitOfWork = unitOfWork;
        }

        public TEntity Get(TId id)
        {
            lock (_syncRoot)
            {
                return GetOrAdd(
                    id,
                    guid =>
                    {
                        var model = _storageAdapter.Transaction(tw => tw.Fetch(guid, DaoType));
                        var converter = CreateConverter();
                        var result = converter.Dao2Entity<TEntity>(model);

                        return result;
                    });
            }
        }

        public IReadOnlyCollection<TEntity> GetAll()
        {
            lock (_syncRoot)
            {
                if (_allFetched)
                {
                    return _cache.Values.ToList();
                }

                var all = _storageAdapter.Transaction(t =>
                    t.InvokeGenericMethod<ICollection<Dao<TId>>>(typeof(TId), nameof(t.FetchAll), DaoType));

                var converter = CreateConverter();
                var result = all.Select(x => converter.Dao2Entity<TEntity>(x)).ToList();

                RefreshCache(result);
                _allFetched = true;

                return result;
            }
        }

        public void Delete(TId id)
        {
            lock (_syncRoot)
            {
                _unitOfWork.Delete(id, DaoType);
                InvalidateCache(id);
            }
        }

        public void DeleteAll()
        {
            lock (_syncRoot)
            {
                _unitOfWork.DeleteAll(DaoType);
                InvalidateCache();
            }
        }

        public TEntity FindSingle(Func<TEntity, bool> predicate)
        {
            lock (_syncRoot)
            {
                var aggregate = GetAll().SingleOrDefault(predicate);
                return aggregate;
            }
        }

        public ICollection<TEntity> FindAll(Func<TEntity, bool> predicate)
        {
            lock (_syncRoot)
            {
                var aggregates = GetAll().Where(predicate).ToList();
                return aggregates;
            }
        }

        public TEntity Clone(TEntity entity)
        {
            var converter = CreateConverter();

            var dao = converter.InvokeGenericMethod(DaoType, Entity2DaoMethodName, entity);
            var clone = converter.InvokeGenericMethod(typeof(TEntity), Dao2EntityMethodName, dao) as TEntity;

            Debug.Assert(clone != null);
            clone.Id = IdGenerator.NewId<TId>();

            return clone;
        }

        public void Save(TEntity entity)
        {
            // [kk] here we can check entity consistency in the future

            lock (_syncRoot)
            {
                var events = entity.ExtractEvents();
                _unitOfWork.TrackEvents(events);

                var converter = CreateConverter();
                var model = converter.InvokeGenericMethod(DaoType, Entity2DaoMethodName, entity);

                _unitOfWork.Save(model, DaoType);

                RefreshCache(entity);
            }
        }

        private static IEntityConverter<TId> CreateConverter()
        {
            return GlobalConfigurationExtension.ResolveEntityConverterFactory().Create<TId>();
        }

        private TEntity GetOrAdd(TId id, Func<TId, TEntity> factory)
        {
            if (_cache.ContainsKey(id))
            {
                return _cache[id];
            }

            var result = factory(id);
            RefreshCache(result);

            return result;
        }

        private void RefreshCache(IEnumerable<TEntity> aggregates)
        {
            foreach (var aggregate in aggregates)
            {
                RefreshCache(aggregate);
            }
        }

        private void RefreshCache(TEntity aggregate)
        {
            if (aggregate == null) return;

            _cache[aggregate.Id] = aggregate;
        }

        private void InvalidateCache(TId id)
        {
            if (_cache.ContainsKey(id) == false)
            {
                return;
            }

            _cache.Remove(id);
            _allFetched = false;
        }

        private void InvalidateCache()
        {
            _cache.Clear();
            _allFetched = false;
        }
    }
}