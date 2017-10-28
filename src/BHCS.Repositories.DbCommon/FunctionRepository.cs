using BHCS.Domain.Models.Menus;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class FunctionRepository : FastDbCommonRepository<Function>, IFunctionRepository
    {
        public bool IsExist(Guid pageId, string name)
        {
            Ensure.NotNull(pageId, "所属页面不能为空！");
            Ensure.NotNullOrWhiteSpace(name, "功能名称不能为空！");

            if (Get(p => p.PageId == pageId && p.Name == name) != null) return true;
            else return false;
        }
    }
}
