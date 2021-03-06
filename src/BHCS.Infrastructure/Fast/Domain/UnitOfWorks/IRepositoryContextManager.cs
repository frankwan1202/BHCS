﻿/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/3/30 星期三 11:08:18
 * 版本号：v1.0
 * 本类主要用途描述：
 *  -------------------------------------------------------------------------*/

using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Domain.UnitOfWorks
{
    /// <summary>
    /// <see cref="IRepositoryContextManager"/>
    /// </summary>
    public interface IRepositoryContextManager
    {
        IRepositoryContext Create();

        IRepositoryContext Current { get; }
    }
}
