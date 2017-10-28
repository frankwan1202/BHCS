using System;
using BHCS.Infrastructure.Enums;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;

namespace BHCS.Domain.Models.Users
{
    public class User:AggregateRoot,ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public User()
        {
        }

        public User(string userName, string account, string password, string mobile, string email, Guid roleId)
        {
            Ensure.NotNullOrWhiteSpace(userName, "姓名不能为空！");
            Ensure.NotNullOrWhiteSpace(account, "账号不能为空！");
            Ensure.NotNullOrWhiteSpace(password, "邮箱不能为空！");
            Ensure.NotNullOrWhiteSpace(mobile, "手机不能为空！");
            Ensure.NotNullOrWhiteSpace(email, "密码不能为空！");
            Ensure.NotNull(roleId, "角色不能为空！");
            Ensure.GrandThan(password.Length, 6, "密码不能低于6位！");

            UserId = Guid.NewGuid();
            UserName = userName;
            Account = account;
            Password = password;
            Mobile = mobile;
            Email = email;
            RoleId = roleId;
            State =  AccountState.Enabled;
        }

        public Guid UserId{get;set;}

        public string UserName{get;set;}

        public string Account{get;set;}

        public string Password{get;set;}

        public string Mobile{get;set;}

        public string Email{get;set;}

        public Guid RoleId{get;set;}

        public AccountState State{get;set;}

        public DateTime CreateTime{get;set;}

        public Guid CreateBy{get;set;}

        public string CreateByName{get;set;}

        public DateTime UpdateTime{get;set;}

        public Guid UpdateBy{get;set;}

        public string UpdateByName{get;set;}

        public bool IsDelete { get; set; }

        public void MD5Password()
        {
            Password = Password.ToMD5WithLower();
        }
    }
}