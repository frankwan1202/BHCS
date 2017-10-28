using BHCS.Domain.Models.Users;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class StudentRepository : FastDbCommonRepository<Student>, IStudentRepository
    {
        public bool IsExistStudent(string studentNo)
        {
            Ensure.NotNullOrWhiteSpace(studentNo, "学号不能为空！");
            if (Get(p => p.StudentNo == studentNo) != null) return true;
            else return false;
        }
    }
}
