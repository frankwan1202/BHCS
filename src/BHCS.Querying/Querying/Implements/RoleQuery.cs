using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastCommon.Components;
using System.Linq;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Domain.Models;

namespace BHCS.Querying.Querying.Implements
{
    public class RoleQuery : IRoleQuery
    {
        public IPaged<RoleReadModel> GetPage(int pageIndex, int pageSize)
        {
            var queryBuilder = new QueryBuilder().FromTable("Role").AddWhere("IsDelete=0").AddPageInfo(pageSize,pageIndex);
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<RoleReadModel>(queryBuilder);
        }

        public IList<RoleReadModel> GetRoleList(bool isLoadDeletedData = false)
        {
            if (!isLoadDeletedData) return QueryEnvironment.Current.GetFromSection<Role>().Where(p => p.IsDelete == false).OrderByDescending(p => p.CreateTime).ToList<RoleReadModel>();
            return QueryEnvironment.Current.GetFromSection<Role>().OrderByDescending(p => p.CreateTime).ToList<RoleReadModel>();
        }
    }
}
