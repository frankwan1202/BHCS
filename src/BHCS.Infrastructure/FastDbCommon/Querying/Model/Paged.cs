/*-------------------------------------------------------------------------
 * 作者：FRind
 * 创建时间： 2016/4/26 星期二 12:25:33
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
    /// <see cref="Paged"/>
    /// </summary>
    public class Paged<T>:IPaged<T>where T:class
    {
        public Paged(int? totalPage, int? pageNumber, long? totalCount,int? pageSize, IList<T> data)
        {
            this.TotalPage = totalPage;
            this.PageNumber = pageNumber;
            this.TotalCount = totalCount;
            this.PageSize = pageSize;
            this.Data = data;
        }

        public int? TotalPage { get; private set; }

        public long? TotalCount { get; private set; }

        public int? PageNumber { get; private set; }

        public int? PageSize { get; private set; }

        public IList<T> Data { get; private set; }
    }
}
