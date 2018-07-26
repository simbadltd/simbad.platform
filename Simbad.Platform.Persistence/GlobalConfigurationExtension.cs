using System;
using Simbad.Platform.Core;
using Simbad.Platform.Core.Dependencies;
using Simbad.Platform.Persistence.Converting;

namespace Simbad.Platform.Persistence
{
    public static class GlobalConfigurationExtension
    {
        public static Global.Configuration EnablePersistence(this Global.Configuration configuration, Action<PersistenceConfiguration> persistenceSetup)
        {
            Global.Ioc.RegisterSingle(TypeRegistration.For<SimpleParameterlessCtorConverter, IConverter>(Lifetime.PerLifetimeScope));
            
            persistenceSetup(new PersistenceConfiguration(configuration));

            return configuration;
        }
    }
}