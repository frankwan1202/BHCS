using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql
{
    public class MySqlEntitySqlCache : EntitySqlCache
    {
        private readonly Database _database;

        public MySqlEntitySqlCache(Database database)
        {
            _database = database;
        }

        protected override EntitySql CreateEntitySql(Type entityType)
        {
            return new MySqlEntitySql(entityType, _database);
        }
    }
}
