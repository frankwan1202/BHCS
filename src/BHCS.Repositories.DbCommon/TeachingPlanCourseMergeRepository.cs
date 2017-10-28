using BHCS.Domain.Models.TeachingPlans;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class TeachingPlanCourseMergeRepository:FastDbCommonRepository<TeachingPlanCourseMerge>,ITeachingPlanCourseMergeRepository
    {
    }
}
