using System;
using System.Collections.Generic;
using System.Reflection;
using Simbad.Platform.Core.Substance;
using Xunit;

namespace Simbad.Platform.Persistence.Tests
{
    public sealed class TestConverter : IConverter
    {
        public TDao BusinessObject2Dao<TDao>(BusinessObject businessObject) where TDao : Dao
        {
            return Map<BusinessObject, TDao>(businessObject);
        }

        public TBusinessObject Dao2BusinessObject<TBusinessObject>(Dao dao) where TBusinessObject : BusinessObject
        {
            return Map<Dao, TBusinessObject>(dao);
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