using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Infrastructure.FastDbCommon.Querying.Enum;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Infrastructure
{
    public interface IInterpreterProvider
    {
        char LeftToken { get; }

        char RightToken { get; }

        DatabaseType DatabaseType { get; }

        DbProviderFactory DbProviderFactory { get; }

        string BuildTableName(string name, string userName);

        FromSection CreatePageFromSection(FromSection fromSection, int startIndex, int endIndex);

        void PrepareCommand(DbCommand cmd);

        string BuildParameterName(string name);

        string InterpreterQueryBuilder(QueryBuilder queryBuilder);
    }
}
