using System;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Substance.IdGeneration
{
    public static class IdGenerator
    {
        private static readonly Dictionary<Type, Func<object>> Generators = new Dictionary<Type, Func<object>>();

        public static TId NewId<TId>()
        {
            var idType = typeof(TId);

            if (Generators.ContainsKey(idType) == false)
            {
                throw new InvalidOperationException(
                    $"There is no id generator for <{idType}>. Please, register id generator with <{nameof(RegisterIdGenertor)}>");
            }

            var result = (TId) Generators[idType]();
            return result;
        }

        internal static void RegisterIdGenertor<TId>(Func<object> idGenerator)
        {
            var idType = typeof(TId);

            if (Generators.ContainsKey(idType))
            {
                throw new InvalidOperationException($"We already have id generator for <{idType}>");
            }

            Generators[idType] = idGenerator;
        }
    }
}