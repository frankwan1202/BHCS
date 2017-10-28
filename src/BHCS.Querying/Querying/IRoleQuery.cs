using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.Querying
{
    public interface IRoleQuery
    {
        IPaged<RoleReadModel> GetPage(int pageIndex, int pageSize);

        IList<RoleReadModel> GetRoleList(bool isLoadDeletedData = false);
    }
}
