using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface ITeacherQuery
    {
        IPaged<TeacherReadModel> GetTeacherPage(int pageIndex, int pageSize);
    }
}
