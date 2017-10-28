using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Querying.Querying.Implements
{
    public class ClassRoomQuery : IClassRoomQuery
    {
        public IPaged<ClassRoomReadModel> GetPage(int pageIndex, int pageSize)
        {
            string strsql = $@"select cr.*,cb.Name BuildingName,cb.Address BuildingAddress from ClassRoom cr inner join ClassBuilding cb on cr.ClassBuildingId=cb.BuildingId where cr.IsDelete=0 order by cr.CreateTime desc";
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<ClassRoomReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize,pageIndex));
        }
    }
}
