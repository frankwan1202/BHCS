using BHCS.Domain.Models;
using BHCS.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Repositories.DbCommon
{
    public class RoleMenuRepository :FastDbCommonRepository<RoleMenu>, IRoleMenuRepository
    {
    }
}
