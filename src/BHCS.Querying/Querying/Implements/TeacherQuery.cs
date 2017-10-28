using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.FastDbCommon.Querying.Model;
using BHCS.Querying.ReadModels;
using BHCS.Infrastructure.FastDbCommon.Querying;

namespace BHCS.Querying.Querying.Implements
{
    public class TeacherQuery : ITeacherQuery
    {
        public IPaged<TeacherReadModel> GetTeacherPage(int pageIndex, int pageSize)
        {
            string strsql = $@"select t.*,u.Account,u.UserName,m.Name MajorName,u.Email,u.Mobile,r.RoleName,u.CreateTime,u.CreateBy,u.CreateByName,
                                        u.UpdateTime,u.UpdateBy,u.UpdateByName,u.State
                                from Teacher t inner join User u on t.UserId=u.UserId inner join Major m on t.MajorId=m.MajorId 
                                        inner join Role r on u.RoleId=r.RoleId 
                                where u.IsDelete=0 
                                order by u.CreateTime desc";
            return QueryEnvironment.Current.GetQuickSqlSection().ToPage<TeacherReadModel>(new QueryBuilder().FromTable(strsql).AddPageInfo(pageSize,pageIndex));
        }
    }
}
