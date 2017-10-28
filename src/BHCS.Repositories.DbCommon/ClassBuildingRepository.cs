using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class ClassBuildingRepository : FastDbCommonRepository<ClassBuilding>, IClassBuildingRepository
    {
        public bool IsExist(string name)
        {
            Ensure.NotNull(name, "教学楼名称不能为空！");
            if (Get(p => p.Name == name) != null) return true;
            else return false;
        }
    }
}
