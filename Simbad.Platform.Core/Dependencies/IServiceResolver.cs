using System;

namespace Simbad.Platform.Core.Dependencies
{
    public interface IServiceResolver
    {
        TAbstraction Resolve<TAbstraction>(Type type) where TAbstraction : class;
    }
}