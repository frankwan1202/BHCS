using MySql.Data.MySqlClient;
using BHCS.Infrastructure.FastDbCommon.Infrastructure;
using SkyCreative.QuickApp.Data.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure.MySql
{
    public class MySqlEntitySql : EntitySql
    {
        public MySqlEntitySql(Type entityType,Database database) : base(entityType,database)
        {
        }

        protected override IList<string> CreateKeys()
        {
            IList<String> keys = new List<String>();
            IDataReader reader = Database.ExecuteDataReader(new MySqlCommand("SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE TABLE_NAME='"+TableName+"' and CONSTRAINT_NAME='primary'"));
            while (reader.Read())
            {
                keys.Add(reader[0].ToString().ToLower());
            }
            reader.Close();

            return keys;
        }
    }
}
