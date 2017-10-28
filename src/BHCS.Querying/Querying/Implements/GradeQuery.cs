using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models;

namespace BHCS.Querying.Querying.Implements
{
    public class GradeQuery : IGradeQuery
    {
        public IList<GradeReadModel> GetList(bool isLoadDeletedData = false)
        {
            if (!isLoadDeletedData) return QueryEnvironment.Current.GetFromSection<Grade>().Where(p => p.IsDelete == false).OrderByDescending(p => p.CreateTime).ToList<GradeReadModel>();
            return QueryEnvironment.Current.GetFromSection<Grade>().OrderByDescending(p => p.CreateTime).ToList<GradeReadModel>();
        }

        public IPaged<GradeReadModel> GetPage(int pageIndex, int pageSize)
        {
            return QueryEnvironment.Current.GetFromSection<Grade>().Where(p =>!p.IsDelete).OrderByDescending(p => p.CreateTime).ToPage<GradeReadModel>(pageSize,pageIndex);
        }
    }
}
