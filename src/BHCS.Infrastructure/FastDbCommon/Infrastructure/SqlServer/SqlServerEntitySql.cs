using MySql.Data.MySqlClient;
using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using SkyCreative.QuickApp.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.SqlServer
{
    public class SqlServerEntitySql : EntitySql
    {
        public SqlServerEntitySql(Type entityType,Database database) : base(entityType,database)
        {
        }

        protected override IList<string> CreateKeys()
        {
            IList<string> keys = new List<string>();
            IDataReader reader = Database.ExecuteDataReader(new SqlCommand("select COLUMN_NAME as ColumnName from INFORMATION_SCHEMA.KEY_COLUMN_USAGE where TABLE_NAME='" + TableName + "'"));
            while (reader.Read())
            {
                keys.Add(reader[0].ToString().ToLower());
            }
            reader.Close();

            return keys;
        }
    }
}
