using Simbad.Platform.Core;
using Simbad.Platform.Core.Dependencies;
using Simbad.Platform.Persistence.Storage;

namespace Simbad.Platform.Persistence.Sqlite
{
    public static class PersistenceConfigurationExtension
    {
        internal const string DbPathParameterName = "Simbad.Platform.Persistence.Sqlite.DbPath";

        public static PersistenceConfiguration UseSqlite(this PersistenceConfiguration configuration, string dbPath)
        {
            Global.Ioc.RegisterSingle(TypeRegistration.For<SqliteStorageAdapter, IStorageAdapter>(Lifetime.PerLifetimeScope));

            configuration.GlobalConfiguration.SetParameter(DbPathParameterName, dbPath);

            return configuration;
        }
    }
}