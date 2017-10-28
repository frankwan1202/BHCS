using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql;
using BHCS.Infrastructure.FastDbCommon.Persistents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BHCS.Infrastructure.FastDbCommon.Infrastructure.SqlServer;
using BHCS.Infrastructure.FastCommon.Components;

namespace BHCS.Infrastructure.FastCommon.Configurations
{
    public static class ConfigurationExetension
    {
        public static Configuration UseMySql(this Configuration configuration, Database databaseInstance)
        {
            ObjectContainer.Register<EntitySqlCache, MySqlEntitySqlCache>();

            RegisterCommon(configuration, databaseInstance, typeof(MySqlInterpreterProvider));

            return configuration;
        }

        public static Configuration UseSqlServer(this Configuration configuration, Database databaseInstance)
        {
            ObjectContainer.Register<EntitySqlCache, SqlServerEntitySqlCache>();

            RegisterCommon(configuration, databaseInstance, typeof(SqlServerInterpreterProvider));

            return configuration;
        }

        private static void RegisterCommon(Configuration configuration, Database databaseInstance,Type interpreterProviderType)
        {
            ObjectContainer.RegisterInstance<IInterpreterExecutor>( databaseInstance);
            ObjectContainer.Register(typeof(IInterpreterProvider),interpreterProviderType);

            ObjectContainer.RegisterInstance(databaseInstance, typeof(Database));
            ObjectContainer.Register<IPersistentContext, PersistentContext>( LifeScope.Transient);
        }
    }
}
