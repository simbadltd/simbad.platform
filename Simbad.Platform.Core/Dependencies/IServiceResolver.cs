using System;
using System.Collections;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Dependencies
{
    public interface IServiceResolver
    {
        T Resolve<T>() where T : class;

        object Resolve(Type abstractionType);
        
        IEnumerable<T> ResolveMany<T>();
        
        IEnumerable ResolveMany(Type abstractionType);
    }
}