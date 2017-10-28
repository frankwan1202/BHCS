/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/3/30 星期三 15:29:11
 * 版本号：v1.0
 * 本类主要用途描述：
 *  -------------------------------------------------------------------------*/

using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Domain.UnitOfWorks
{
    /// <summary>
    /// <see cref="CurrentRepositoryContextProvider"/>
    /// </summary>
    public class CurrentRepositoryContextProvider:ICurrentRepositoryContextProvider
    {
        private static String ContextKey = "BHCS.Infrastructure.FastCommon.RepositoryContext.Current";

        private static AsyncLocal<IRepositoryContext> _repositoryContextLocal = new AsyncLocal<IRepositoryContext>();

        public CurrentRepositoryContextProvider()
        {
        }

        public IRepositoryContext GetCurrentRepositoryContext()
        {
            return _repositoryContextLocal.Value;
        }

        public void SetCurrentRepositoryContext(IRepositoryContext repositoryContext)
        {
            _repositoryContextLocal.Value = repositoryContext;
        }

        public IRepositoryContext Current
        {
            get { return this.GetCurrentRepositoryContext(); }
            set { this.SetCurrentRepositoryContext(value); }
        }
    }
}
