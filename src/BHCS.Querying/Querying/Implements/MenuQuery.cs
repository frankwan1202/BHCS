using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models.Menus;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;

namespace BHCS.Querying.Querying.Implements
{
    public class MenuQuery : IMenuQuery
    {
        public IList<MenuReadModel> GetList()
        {
            return QueryEnvironment.Current.GetFromSection<Menu>().ToList<MenuReadModel>();
        }

        public IPaged<MenuReadModel> GetPage(int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<Menu>().OrderBy(p => p.Sort).ToPage<MenuReadModel>(pageSize, pageIndex);
        }
    }
}
