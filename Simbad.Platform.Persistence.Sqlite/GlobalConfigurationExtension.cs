using Simbad.Platform.Core;

namespace Simbad.Platform.Persistence.Sqlite
{
    public static class GlobalConfigurationExtension
    {
        internal const string DbPathParameterName = "Simbad.Platform.Persistence.Sqlite.DbPath";

        public static Global.Configuration UseSqlitePersistence(this Global.Configuration configuration, string dbPath)
        {
            configuration.UseStorageAdapter<SqliteStorageAdapter>();
            configuration.SetParameter(DbPathParameterName, dbPath);

            return configuration;
        }
    }
}