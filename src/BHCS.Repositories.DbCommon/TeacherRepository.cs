using BHCS.Domain.Models.Users;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class TeacherRepository: FastDbCommonRepository<Teacher>,ITeacherRepository
    {
    }
}
