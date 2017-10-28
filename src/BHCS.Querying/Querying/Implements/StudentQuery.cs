using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Querying.Querying.Implements
{
    public class StudentQuery : IStudentQuery
    {
        public IPaged<StudentReadModel> GetPaged(int pageIndex, int pageSize)
        {
            string strsql = $@"select s.StudentId,s.StudentNo,u.Account,u.UserName,s.MajorId,m.`Name` MajorName,c.ClassesId,c.`Name` ClassesName,
				                        s.GradeId,g.Name GradeName,u.Email,u.Mobile,u.CreateTime,u.CreateBy,u.CreateByName,u.UpdateTime,
				                        u.UpdateBy,u.UpdateByName,u.State 
                                from Student s inner join `User` u on s.UserId=u.UserId inner join Grade g on s.GradeId=g.GradeId 
                                inner join Classes c on s.ClassesId=c.ClassesId inner join Major m on s.MajorId=m.MajorId 
                                where u.IsDelete=0 ";
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<StudentReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize, pageIndex));
        }
    }
}
