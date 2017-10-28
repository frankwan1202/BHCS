/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/5/13 星期五 11:54:08
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
    /// <see cref="QueryPagedFilter"/>
    /// </summary>
    public abstract class QueryPagedRequest : IQueryPagedRequest
    {
        public QueryPagedRequest(int pageNumber = 1, int pageSize = -1)
        {
            this.PageNumber = pageNumber;
            this.PageSize=pageSize;
            if (pageSize == -1)
            {
                PageSize =20;
            }
        }

        public int PageSize { get; set; }

        public int PageNumber { get;  set; }
    }
}
