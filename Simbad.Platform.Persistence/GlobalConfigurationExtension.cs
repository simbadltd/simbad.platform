using System;
using Simbad.Platform.Core;

namespace Simbad.Platform.Persistence
{
    public static class GlobalConfigurationExtension
    {
        private const string EntityConverterFactoryParameterName = "Simbad.Platform.Persistence.EntityConverterFactory";

        private const string StorageAdapterParameterName = "Simbad.Platform.Persistence.StorageAdapter";

        public static IEntityConverterFactory ResolveEntityConverterFactory()
        {
            var type = Global.Parameter<Type>(EntityConverterFactoryParameterName);
            var entityConverterFactory = Global.ResolveService<IEntityConverterFactory>(type);
            return entityConverterFactory;
        }

        public static IStorageAdapter ResolveStorageAdapter()
        {
            var type = Global.Parameter<Type>(StorageAdapterParameterName);
            var storageAdapter = Global.ResolveService<IStorageAdapter>(type);
            return storageAdapter;
        }

        public static Global.Configuration UseEntityConverterFactory<T>(this Global.Configuration configuration)
            where T : IEntityConverterFactory
        {
            configuration.SetParameter(EntityConverterFactoryParameterName, typeof(T));

            return configuration;
        }

        public static Global.Configuration UseStorageAdapter<T>(this Global.Configuration configuration)
            where T : IStorageAdapter
        {
            configuration.SetParameter(StorageAdapterParameterName, typeof(T));

            return configuration;
        }
    }
}