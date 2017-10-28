using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Querying.Querying.Implements
{
    public class CourseQuery : ICourseQuery
    {
        public IPaged<CourseReadModel> GetPage(int pageIndex, int pageSize)
        {
            string strsql = $@"select c.CourseId,c.Name,c.NatureId,cn.Name NatureName,c.Hours,c.MajorId,m.Name MajorName,
                                        c.CreateTime,c.CreateBy,c.CreateByName 
                                from Course c inner join CourseNature cn on c.NatureId=cn.NatureId inner join Major m on c.MajorId=m.MajorId";

            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<CourseReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize, pageIndex));
        }
    }
}
