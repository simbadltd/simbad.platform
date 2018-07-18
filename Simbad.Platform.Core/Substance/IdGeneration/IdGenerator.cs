using System;
using System.Collections.Generic;

namespace Simbad.Platform.Core.Substance.IdGeneration
{
    public static class IdGenerator
    {
        private static Func<Guid> _generator = Guid.NewGuid;

        public static Guid NewId()
        {
            return _generator();
        }

        internal static void RegisterIdGenertor(Func<Guid> idGenerator)
        {
            _generator = idGenerator;
        }
    }
}