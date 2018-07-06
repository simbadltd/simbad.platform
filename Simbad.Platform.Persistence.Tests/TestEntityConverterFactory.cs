using System;

namespace Simbad.Platform.Persistence.Tests
{
    public sealed class TestEntityConverterFactory : IEntityConverterFactory
    {
        public IEntityConverter<TId> Create<TId>()
        {
            if (typeof(TId) == typeof(Guid))
            {
                return new TestEntityConverter() as IEntityConverter<TId>;
            }

            throw new NotImplementedException();
        }
    }
}