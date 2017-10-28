/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/5/13 星期五 11:52:54
 * 版本号：v1.0
 * 本类主要用途描述：
 *  -------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkyCreative.ORM.SqlQuery.QueryRequest
{
    /// <summary>
    /// <see cref="IQueryPagedFilter"/>
    /// </summary>
    public interface IQueryPagedRequest:IQueryRequest
    {
        int PageSize { get; }

        int PageNumber { get; }
    }
}
