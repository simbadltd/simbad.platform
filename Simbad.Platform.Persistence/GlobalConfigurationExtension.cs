using Simbad.Platform.Core;
using Simbad.Platform.Core.Dependencies;

namespace Simbad.Platform.Persistence
{
    public static class GlobalConfigurationExtension
    {
        /// <summary>
        /// Enables very simple persistence based on the in-memory storage. It is good option for prototyping, MVPs. But it should not be used in any production
        /// ready functionality. Transactions are NOT supported in this storage.
        /// </summary>
        public static Global.Configuration UseInMemoryPersistence(this Global.Configuration configuration)
        {
            Global.Ioc.RegisterSingle(TypeRegistration.For<InMemoryStorageAdapter, IStorageAdapter>(Lifetime.PerLifetimeScope));
            
            return configuration;
        }        
    }
}