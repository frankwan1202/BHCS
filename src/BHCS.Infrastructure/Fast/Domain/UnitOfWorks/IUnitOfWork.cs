using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Domain.UnitOfWorks
{
    public interface IUnitOfWork:IDisposable
    {
        bool IsCommited { get; }

        bool IsDisposed { get; }

        void Commit();

        void Rollback();
    }
}
