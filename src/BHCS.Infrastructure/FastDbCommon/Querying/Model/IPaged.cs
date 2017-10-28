/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/4/26 星期二 12:23:24
 * 版本号：v1.0
 * 本类主要用途描述：
 *  -------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastDbCommon.Querying.Model
{
    /// <summary>
    /// <see cref="IPaged"/>
    /// </summary>
    public interface IPaged<T>where T:class
    {
        int? TotalPage { get; }

        long? TotalCount { get; }

        int? PageNumber { get; }

        int? PageSize { get; }

        IList<T> Data { get; }
    }
}
