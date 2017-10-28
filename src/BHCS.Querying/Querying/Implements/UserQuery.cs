using System;
using System.Collections.Generic;
using System.Text;
using BHCS.Infrastructure.Middlewares;
using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.FastDbCommon.Querying;
using BHCS.Domain.Models.Users;
using BHCS.Domain.Models;
using BHCS.Domain.Models.Menus;

namespace BHCS.Querying.Querying.Implements
{
    public class UserQuery : IUserQuery
    {
        public SignMember GetLoginUserInfo(Guid userId)
        {
            Ensure.NotNull(userId, "用戶ID不能为空！");
            var user = QueryEnvironment.Current.GetFromSection<User>().Where(p => p.UserId == userId).ToFirst();
            Ensure.NotNull(user, "用户信息获取失败！");
            var role = QueryEnvironment.Current.GetFromSection<Role>().Where(p => p.RoleId == user.RoleId).ToFirst();
            Ensure.NotNull(role, "用户角色信息获取失败！");
            var funces=QueryEnvironment.Current.GetQuickSqlSection().ToList<Function>($@"select f.* from RoleMenu rm inner join Function f on rm.FuncId=f.FuncId where rm.RoleId='{role.RoleId}'");
            var pages = QueryEnvironment.Current.GetQuickSqlSection().ToList<Function>($@"select p.* from RoleMenu rm inner join Page p on rm.PageId=p.PageId where rm.RoleId='{role.RoleId}'");
            if (funces == null) funces = new List<Function>();
            if (pages == null) pages = new List<Function>();
            funces.AddRange(pages);
            return new SignMember(user.UserId,user.Account, user.UserName, role.RoleId, role.RoleName, Convert(funces));
        }

        private IList<SignFunction> Convert(IList<Function> funces)
        {
            var signFunces = new List<SignFunction>();
            if (funces != null && funces.Count > 0)
            {
                foreach (var func in funces)
                {
                    var signFunc = new SignFunction(func.OperationNum, func.Url);
                    signFunces.Add(signFunc);
                }
            }
            return signFunces;
        }
    }
}
