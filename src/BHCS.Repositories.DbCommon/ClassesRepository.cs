using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class ClassesRepository : FastDbCommonRepository<Classes>, IClassesRepository
    {
        public bool IsExist(string classesNo)
        {
            Ensure.NotNullOrWhiteSpace(classesNo, "班号不能为空！");
            if (Get(p => p.ClassesNo == classesNo) != null) return true;
            else return false;
        }
    }
}
