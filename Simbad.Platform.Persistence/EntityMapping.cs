using System;
using System.Collections.Generic;
using Simbad.Platform.Core.Substance;

namespace Simbad.Platform.Persistence
{
    public static class EntityMapping
    {
        private static readonly Dictionary<Type, Type> Entity2Dao = new Dictionary<Type, Type>();

        private static readonly Dictionary<Type, Type> Dao2Entity = new Dictionary<Type, Type>();

        public static Type DaoTypeFor(Type entityType)
        {
            if (Entity2Dao.ContainsKey(entityType) == false)
            {
                throw new InvalidOperationException($"There is no mapping for entity type <{entityType}>");
            }

            return Entity2Dao[entityType];
        }

        public static Type EntityTypeFor(Type daoType)
        {
            if (Dao2Entity.ContainsKey(daoType) == false)
            {
                throw new InvalidOperationException($"There is no mapping for dao type <{daoType}>");
            }

            return Dao2Entity[daoType];
        }

        public static EntityMappingConfiguration Configure()
        {
            return new EntityMappingConfiguration();
        }

        public sealed class EntityMappingConfiguration
        {
            public EntityMappingConfiguration Add<TEntity, TDao, TId>()
                where TDao : Dao<TId> where TEntity : Entity<TId>
            {
                var entityType = typeof(TEntity);
                var daoType = typeof(TDao);

                if (Entity2Dao.ContainsKey(entityType))
                {
                    throw new InvalidOperationException($"We already have mapping for <{entityType}>");
                }

                if (Dao2Entity.ContainsKey(daoType))
                {
                    throw new InvalidOperationException($"We already have mapping for <{daoType}>");
                }

                Entity2Dao[entityType] = daoType;
                Dao2Entity[daoType] = entityType;

                return this;
            }
        }
    }
}