using BHCS.Domain.Models.TeachingPlans;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class TeachingPlanRepository : FastDbCommonRepository<TeachingPlan>, ITeachingPlanRepository
    {
        public bool IsExist(Guid majorId, Guid gradeId)
        {
            Ensure.NotNull(majorId, "专业不能为空！");
            Ensure.NotNull(gradeId, "年级不能为空！");
            if (Get(p => p.MajorId == majorId && p.GradeId == gradeId) != null) return true;
            else return false;
        }
    }
}
