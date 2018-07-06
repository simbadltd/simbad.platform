using System;
using Simbad.Platform.Core.Substance;

namespace Simbad.Platform.Persistence.Sqlite.Tests
{
    public sealed class TestEntity : Entity<Guid>
    {
        public string TestProperty { get; set; }
    }
}