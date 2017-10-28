using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models.Menus;

namespace BHCS.Querying.Querying.Implements
{
    public class PageQuery : IPageQuery
    {
        public IPaged<PageReadModel> GetPage(Guid menuId,int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<Page>().Where(p=>p.MenuId==menuId).ToPage<PageReadModel>(pageSize, pageIndex);
        }
    }
}
