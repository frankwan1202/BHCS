using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.SqlServer
{
    public class SqlServerEntitySqlCache : EntitySqlCache
    {
        private readonly Database _database;

        public SqlServerEntitySqlCache(Database database)
        {
            _database = database;
        }

        protected override EntitySql CreateEntitySql(Type entityType)
        {
            return new SqlServerEntitySql(entityType, _database);
        }
    }
}
