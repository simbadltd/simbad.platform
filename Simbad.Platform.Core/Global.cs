using System;
using System.Collections.Generic;
using System.ComponentModel;
using Simbad.Platform.Core.Dependencies;
using Simbad.Platform.Core.Events;
using Simbad.Platform.Core.Substance.IdGeneration;

namespace Simbad.Platform.Core
{
    public static class Global
    {
        private const string EventDispatcherParameterName = "Simbad.Platform.Core.EventDispatcher";

        private static readonly Dictionary<string, object> Parameters = new Dictionary<string, object>();

        private static IServiceResolver _serviceResolver = new SimpleParameterlessCtorServiceResolver();

        public static T Parameter<T>(string name)
        {
            if (Parameters.ContainsKey(name) == false)
            {
                throw new ArgumentException(
                    $"Cannot found parameter with name <{name}>. Please, ensure that this parameter is initialized before this call.");
            }

            return (T) Parameters[name];
        }

        public static Configuration Configure()
        {
            return new Configuration();
        }

        public static IEventDispatcher ResolveEventDispatcher()
        {
            return ResolveService<IEventDispatcher>(Parameter<Type>(EventDispatcherParameterName));
        }

        public static T ResolveService<T>(Type type) where T : class
        {
            if (_serviceResolver == null)
            {
                throw new InvalidOperationException(
                    $"There is no service resolver registered. Please, setup it with <{nameof(Configuration.ResolveServiceUsing)}> method call.");
            }

            return _serviceResolver.Resolve<T>(type);
        }

        public sealed class Configuration
        {
            public void SetParameter(string name, object value)
            {
                Parameters[name] = value;
            }

            public Configuration RegisterIdGenertor<TId>(Func<object> idGenerator)
            {
                IdGenerator.RegisterIdGenertor<TId>(idGenerator);

                return this;
            }

            public void ResolveServiceUsing(IServiceResolver serviceResolver)
            {
                _serviceResolver = serviceResolver;
            }

            public void UseEventDispatcher<T>()
            {
                SetParameter(EventDispatcherParameterName, typeof(T));
            }
        }
    }
}