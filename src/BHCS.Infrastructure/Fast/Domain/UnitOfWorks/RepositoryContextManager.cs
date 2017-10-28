/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/3/31 星期四 12:04:58
 * 版本号：v1.0
 * 本类主要用途描述：
 *  -------------------------------------------------------------------------*/

using BHCS.Infrastructure.Fast.Domain.Repositories;
using BHCS.Infrastructure.FastCommon.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Domain.UnitOfWorks
{
    /// <summary>
    /// <see cref="RepositoryContextManager"/>
    /// </summary>
    public class RepositoryContextManager:IRepositoryContextManager
    {
        private ICurrentRepositoryContextProvider currentRepositoryContextProvider;

        public RepositoryContextManager(ICurrentRepositoryContextProvider currentRepositoryContextProvider)
        {
            this.currentRepositoryContextProvider = currentRepositoryContextProvider;
        }

        public IRepositoryContext Current
        {
            get
            {
                return currentRepositoryContextProvider.Current;
            }
        }

        public IRepositoryContext Create()
        {
            IRepositoryContext repositoryContext =ObjectContainer.Resolve<IRepositoryContext>();
            this.currentRepositoryContextProvider.Current = repositoryContext;
            return repositoryContext;
        }
    }
}
