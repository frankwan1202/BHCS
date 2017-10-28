using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models;

namespace BHCS.Querying.Querying.Implements
{
    public class ClassBuildingQuery : IClassBuildingQuery
    {
        public IList<ClassBuildingReadModel> GetList(bool isLoadDeleted = false)
        {
            if (isLoadDeleted) return QueryEnvironment.Current.GetFromSection<ClassBuilding>().OrderByDescending(p => p.CreateTime).ToList<ClassBuildingReadModel>();
            else return QueryEnvironment.Current.GetFromSection<ClassBuilding>().Where(p => !p.IsDelete).OrderByDescending(p => p.CreateTime).ToList<ClassBuildingReadModel>();
        }

        public IPaged<ClassBuildingReadModel> GetPage(int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<ClassBuilding>().OrderByDescending(p => p.CreateTime).ToPage<ClassBuildingReadModel>(pageSize, pageIndex);
        }
    }
}
