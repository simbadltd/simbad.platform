using System;

namespace Simbad.Platform.Core.Dependencies
{
    public interface IServiceResolver
    {
        TAbstraction Resolve<TAbstraction>(Type implementation) where TAbstraction : class;
    }
}