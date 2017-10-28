using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class CourseNatureRepository : FastDbCommonRepository<CourseNature>, ICourseNatureRepository
    {
        public bool IsExist(string natureName)
        {
            Ensure.NotNullOrWhiteSpace(natureName, "课程性质名称不能为空！");
            if (Get(p => p.Name == natureName) != null) return true;
            else return false;
        }
    }
}
