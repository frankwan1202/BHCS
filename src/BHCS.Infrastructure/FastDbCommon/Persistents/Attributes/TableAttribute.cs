using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Attributes
{
    /// <summary>
    /// 数据表标识
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class TableAttribute:Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="schema">前缀</param>
        /// <param name="primaryKey">主键</param>
        public TableAttribute(string table):this(table,null,null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="schema">前缀</param>
        /// <param name="primaryKey">主键</param>
        public TableAttribute(string table, string schema):this(table,schema,null)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="schema">前缀</param>
        /// <param name="primaryKey">主键</param>
        public TableAttribute(string table, string[] primaryKey)
            : this(table, null, primaryKey)
        {
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="table">表名</param>
        /// <param name="schema">前缀</param>
        /// <param name="primaryKey">主键</param>
        public TableAttribute(string table, string schema, string[] primaryKey)
        {
            this.Table = table;
            this.Schema = schema;
            this.PrimaryKey = primaryKey;
        }

        /// <summary>
        /// 表名
        /// </summary>
        public string Table { get; private set; }

        /// <summary>
        /// 前缀
        /// </summary>
        public string Schema { get; private set; }

        /// <summary>
        /// 主键
        /// </summary>
        public IList<string> PrimaryKey { get; private set; }
    }
}
