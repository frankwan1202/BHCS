using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Querying.Querying.Implements
{
    public class TeachingPlanQuery : ITeachingPlanQuery
    {
        public IPaged<TeachingPlanReadModel> GetPage(int pageIndex, int pageSize, bool? isAccept = null)
        {
            string where = "where 1=1 ";
            if (isAccept.HasValue)where+=$"and tp.IsAccept={(isAccept.Value?"1":"0")} ";

            string strsql = $@"select tp.*,m.Name MajorName,g.Name GradeName
                                from TeachingPlan tp inner join Major m on tp.MajorId=m.MajorId 
                                 inner join Grade g on tp.GradeId=g.GradeId {where} 
                                 order by tp.CreateTime desc ";
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<TeachingPlanReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize, pageIndex));
        }
    }
}
