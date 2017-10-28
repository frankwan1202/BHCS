using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.Domain.UnitOfWorks
{
    public abstract class UnitOfWork : IUnitOfWork
    {
        private bool _disposing = false;

        public bool IsCommited { get; private set; }

        public bool IsDisposed => _disposing;

        public void Commit()
        {
            DoCommit();
            IsCommited = true;
        }

        public void Dispose()
        {
            if (_disposing) return;
            DoDispose();
            _disposing = true;
        }

        public void Rollback()
        {
            DoRollback();
        }

        protected abstract void DoCommit();

        protected abstract void DoRollback();

        protected abstract void DoDispose();
    }
}
