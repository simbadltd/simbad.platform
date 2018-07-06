using System;
using System.Collections.Generic;
using System.Reflection;
using Simbad.Platform.Core.Substance;
using Xunit;

namespace Simbad.Platform.Persistence.Tests
{
    public sealed class TestEntityConverter : IEntityConverter<Guid>
    {
        public TDao Entity2Dao<TDao>(Entity<Guid> entity) where TDao : Dao<Guid>
        {
            return Map<Entity<Guid>, TDao>(entity);
        }

        public TEntity Dao2Entity<TEntity>(Dao<Guid> dao) where TEntity : Entity<Guid>
        {
            return Map<Dao<Guid>, TEntity>(dao);
        }

        private static TDest Map<TSrc, TDest>(TSrc src)
        {
            var dest = Activator.CreateInstance<TDest>();

            var srcProps = src.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var values = new Dictionary<string, object>();

            foreach (var p in srcProps)
            {
                values[p.Name] = p.GetValue(src);
            }

            var destProps = dest.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (var p in destProps)
            {
                if (values.ContainsKey(p.Name))
                {
                    p.SetValue(dest, values[p.Name]);
                }
            }

            return dest;
        }
    }
}