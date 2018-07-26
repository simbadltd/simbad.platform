using System;
using System.Collections.Concurrent;
using System.Linq;

namespace Simbad.Platform.Core.Dependencies
{
    internal sealed class SimpleParameterlessCtorServiceResolver : IServiceResolver
    {
        private readonly ConcurrentBag<Type> _safeTypes = new ConcurrentBag<Type>();

        public TAbstraction Resolve<TAbstraction>(Type implementation) where TAbstraction : class
        {
            EnsureTypeIsSafe(implementation);

            if (implementation.IsDerivedFrom(typeof(TAbstraction)) == false)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of type <{implementation}>, type is not derived from <{typeof(TAbstraction)}>.");
            }

            return Activator.CreateInstance(implementation) as TAbstraction;
        }

        private void EnsureTypeIsSafe(Type type)
        {
            if (_safeTypes.Contains(type))
            {
                return;
            }

            if (type.IsClass == false)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of type <{type}>, type is not a class.");
            }

            if (type.IsAbstract)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of type <{type}>, type is an abstract class.");
            }

            if (type.HasParameterlessCtor() == false)
            {
                throw new InvalidOperationException(
                    $"Cannot create instance of type <{type}>, type has no parameterless ctor.");
            }

            _safeTypes.Add(type);
        }
    }
}