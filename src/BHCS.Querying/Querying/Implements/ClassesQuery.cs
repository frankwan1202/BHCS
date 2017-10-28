using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;

namespace BHCS.Querying.Querying.Implements
{
    public class ClassesQuery : IClassesQuery
    {
        public IList<ClassesReadModel> GetList(bool isLoadDeletedData = false)
        {
            if (!isLoadDeletedData) return QueryEnvironment.Current.GetFromSection<Classes>().Where(p => p.IsDelete == false).OrderByDescending(p => p.CreateTime).ToList<ClassesReadModel>();
            return QueryEnvironment.Current.GetFromSection<Classes>().OrderByDescending(p => p.CreateTime).ToList<ClassesReadModel>();
        }

        public IPaged<ClassesReadModel> GetPage(int pageIndex, int pageSize)
        {
            string strsql = $@"select c.ClassesId,c.ClassesNo,c.`Name`,c.GradeId,g.`Name` GradeName,c.CreateTime,c.CreateBy,c.CreateByName,
				                    c.UpdateTime,c.UpdateBy,c.UpdateByName 
                            from Classes c inner join Grade g on c.GradeId=g.GradeId where c.IsDelete=0";
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<ClassesReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize,pageIndex));
        }
    }
}
