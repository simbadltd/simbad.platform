using System;
using Simbad.Platform.Core.Substance;

namespace Simbad.Platform.Persistence.Sqlite.Tests
{
    public sealed class TestBusinessObject : BusinessObject
    {
        public string TestProperty { get; set; }
    }
}