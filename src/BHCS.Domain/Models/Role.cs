using System;
using BHCS.Infrastructure.Fast.Domain.Models;

namespace BHCS.Domain.Models
{
    public class Role:ICreateAudit,IUpdateAudit,ISoftDelete,IAggregateRoot
    {
        public Role()
        { }

        public Role(string roleName)
        {
            RoleId = Guid.NewGuid();
            RoleName = roleName;
            IsDelete = false;
        }

        public Guid RoleId{get;set;}

        public string RoleName{get;set;}

        public DateTime CreateTime{get;set;}

        public Guid CreateBy{get;set;}

        public string CreateByName{get;set;}

        public DateTime UpdateTime{get;set;}

        public Guid UpdateBy{get;set;}

        public string UpdateByName{get;set;}
        
        public bool IsDelete{get;set;}
    }
}