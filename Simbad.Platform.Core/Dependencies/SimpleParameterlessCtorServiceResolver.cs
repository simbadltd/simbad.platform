using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Simbad.Platform.Core.Dependencies
{
    internal sealed class SimpleParameterlessCtorServiceResolver : IServiceResolver
    {
        private readonly ConcurrentBag<Type> _safeTypes = new ConcurrentBag<Type>();

        public TAbstraction Resolve<TAbstraction>(Type type) where TAbstraction : class
        {
            EnsureTypeIsSafe(type);

            return Activator.CreateInstance(type) as TAbstraction;
        }

        private void EnsureTypeIsSafe(Type type)
        {
            if (_safeTypes.Contains(type))
            {
                return;
            }

            if (type.HasParameterlessCtor() == false)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of type <{type}>, cannot find parameterless ctor in it.");
            }

            _safeTypes.Add(type);
        }
    }
}