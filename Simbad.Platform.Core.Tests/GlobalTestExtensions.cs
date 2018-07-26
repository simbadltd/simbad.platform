using System;
using Simbad.Platform.Core.Dependencies;
using Simbad.Platform.Core.Events;
using Xunit;

namespace Simbad.Platform.Core.Tests
{
    public static class GlobalTestExtensions
    {
        public static Global.Configuration UseEventDispatcherStub(this Global.Configuration configuration)
        {
            Global.Ioc.Register(TypeRegistration.For<EventDispatcherStub, IEventDispatcher>());

            return configuration;
        }
    }
}