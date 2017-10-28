using BHCS.Domain.Models.Menus;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class PageRepository : FastDbCommonRepository<Page>, IPageRepository
    {
        public bool IsExist(Guid menuId, string name)
        {
            Ensure.NotNull(menuId, "菜单名不能为空！");
            Ensure.NotNullOrWhiteSpace(name, "页面名不能为空！");

            if (Get(p => p.MenuId == menuId && p.Name == name) != null) return true;
            else return false;
        }
    }
}
