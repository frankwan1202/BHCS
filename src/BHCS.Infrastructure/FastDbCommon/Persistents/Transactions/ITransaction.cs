using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Persistents.Transactions
{
    public interface ITransaction:IDisposable
    {
        void Begin();

        void Commit();

        void Roolback();

        bool IsCommited { get; }

        IDbTransaction CurrentDbTransaction { get; }

    }
}
