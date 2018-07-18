using System;
using Simbad.Platform.Core;

namespace Simbad.Platform.Persistence
{
    public static class GlobalConfigurationExtension
    {
        private const string ConverterParameterName = "Simbad.Platform.Persistence.ConverterParameterName";

        private const string StorageAdapterParameterName = "Simbad.Platform.Persistence.StorageAdapter";

        public static IConverter ResolveConverter()
        {
            var type = Global.Parameter<Type>(ConverterParameterName);
            var converter = Global.ResolveService<IConverter>(type);
            return converter;
        }

        public static IStorageAdapter ResolveStorageAdapter()
        {
            var type = Global.Parameter<Type>(StorageAdapterParameterName);
            var storageAdapter = Global.ResolveService<IStorageAdapter>(type);
            return storageAdapter;
        }

        public static Global.Configuration UseConverter<T>(this Global.Configuration configuration)
            where T : IConverter
        {
            configuration.SetParameter(ConverterParameterName, typeof(T));

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