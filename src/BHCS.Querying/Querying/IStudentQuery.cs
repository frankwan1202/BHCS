using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IStudentQuery
    {
        IPaged<StudentReadModel> GetPaged(int pageIndex, int pageSize);
    }
}
