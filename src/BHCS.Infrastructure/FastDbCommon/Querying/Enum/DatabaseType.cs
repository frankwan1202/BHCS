using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Enum
{
    public enum DatabaseType
    {
        /// <summary>
        /// SQL Server 
        /// </summary>
        SqlServer = 0,
        /// <summary>
        /// MsAccess
        /// </summary>
        MsAccess = 1,
        /// <summary>
        /// Oracle
        /// </summary>
        Oracle = 2,
        /// <summary>
        /// Sqlite
        /// </summary>
        Sqlite3 = 3,
        /// <summary>
        /// MySql
        /// </summary>
        MySql = 4
    }
}
