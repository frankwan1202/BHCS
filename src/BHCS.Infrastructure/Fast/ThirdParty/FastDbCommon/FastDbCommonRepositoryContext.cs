using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.FastDbCommon.Persistents;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon
{
    public class FastDbCommonRepositoryContext:RepositoryContext, IFastDbCommonRepositoryContext
    {
        private IPersistentContext _context;
        private readonly IUserSession _userSession;

        public FastDbCommonRepositoryContext(IPersistentContext persistentContext)
        {
            _context = persistentContext;
            _userSession = ObjectContainer.Resolve<IUserSession>();
        }

        public IPersistentContext DbContext => _context;

        public override void RegisterInsert<TAggregateRoot>(TAggregateRoot aggregate)
        {
            var insertEntity = aggregate as ICreateAudit;
            if (insertEntity != null)
            {
                insertEntity.CreateBy = _userSession.UserId;
                insertEntity.CreateByName = _userSession.UserName;
                insertEntity.CreateTime = DateTime.Now;
            }
            var updateEntity = aggregate as IUpdateAudit;
            if (updateEntity != null)
            {
                updateEntity.UpdateBy = _userSession.UserId;
                updateEntity.UpdateByName = _userSession.UserName;
                updateEntity.UpdateTime = DateTime.Now;
            }
            _context.Add(aggregate);
        }

        public override void RegisterUpdate<TAggregateRoot>(TAggregateRoot aggregate)
        {
            var updateEntity = aggregate as IUpdateAudit;
            if (updateEntity != null)
            {
                updateEntity.UpdateBy = _userSession.UserId;
                updateEntity.UpdateByName = _userSession.UserName;
                updateEntity.UpdateTime = DateTime.Now;
            }
            _context.Update(aggregate);
        }

        public override void RegisterDelete<TAggregateRoot>(TAggregateRoot aggregate)
        {
            var softDeleteEntity = aggregate as ISoftDelete;
            if (softDeleteEntity != null)
            {
                softDeleteEntity.IsDelete = true;
                RegisterUpdate(aggregate);
                return;
            }
            _context.Delete(aggregate);
        }

        protected override void DoDispose()
        {
            _context.Dispose();
        }

        protected override void DoRollback()
        {
        }

        protected override void DoCommit()
        {
            try
            {
                _context.SaveChange();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
