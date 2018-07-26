using Simbad.Platform.Persistence.Storage;

namespace Simbad.Platform.Persistence.Tests
{
    public sealed class TestDao : Dao
    {
        public string TestProperty { get; set; }
    }
}