using BHCS.Domain.Models.TeachingPlans;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Domain.Repositories
{
    public interface ITeachingPlanRepository:IRepository<TeachingPlan>
    {
        bool IsExist(Guid majorId, Guid gradeId);
    }
}