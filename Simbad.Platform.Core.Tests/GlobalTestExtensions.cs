using System;
using Xunit;

namespace Simbad.Platform.Core.Tests
{
    public static class GlobalTestExtensions
    {
        public static Global.Configuration UseEventDispatcherStub(this Global.Configuration configuration)
        {
            configuration.UseEventDispatcher<EventDispatcherStub>();

            return configuration;
        }
    }
}