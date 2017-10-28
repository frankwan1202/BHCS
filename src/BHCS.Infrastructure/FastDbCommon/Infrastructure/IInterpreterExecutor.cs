using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public interface IInterpreterExecutor
    {
        Object ExecuteScalar(DbCommand command);

        DataTable ExecuteDataTable(DbCommand command);

        IDataReader ExecuteDataReader(DbCommand command);
    }
}
