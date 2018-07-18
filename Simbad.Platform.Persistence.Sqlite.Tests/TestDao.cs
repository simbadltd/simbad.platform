using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Moq;
using Simbad.Platform.Core.Events;

namespace Simbad.Platform.Persistence.Sqlite.Tests
{
    public sealed class TestDao : Dao
    {
        public string TestProperty { get; set; }
    }
}