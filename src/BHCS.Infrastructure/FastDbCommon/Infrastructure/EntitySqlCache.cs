using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public abstract class EntitySqlCache
    {
        private readonly IDictionary<Type, EntitySql> _entitySqlDic = new Dictionary<Type, EntitySql>();

        public EntitySql GetEntitySql(Type entityType)
        {
            EntitySql entitySql;
            if (!_entitySqlDic.TryGetValue(entityType,out entitySql))
            {
                entitySql = CreateEntitySql(entityType);
                _entitySqlDic.Add(entityType, entitySql);
            }

            return entitySql;
        }

        protected abstract EntitySql CreateEntitySql(Type entityType);
    }
}
