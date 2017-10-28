using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class CourseRepository : FastDbCommonRepository<Course>, ICourseRepository
    {
        public bool IsExist(string name, Guid majorId)
        {
            Ensure.NotNullOrWhiteSpace(name, "课程名称不能为空！");
            Ensure.NotNull(majorId, "专业不能为空！");

            if (Get(p => p.Name == name && p.MajorId == majorId) != null) return true;
            else return false;
        }
    }
}
