using System;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;

namespace BHCS.Domain.Models
{
    public class Grade:AggregateRoot,ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public Grade()
        {
        }

        public Grade(string name)
        {
            Ensure.NotNullOrWhiteSpace(name, "年级名称不能为空！");

            GradeId = Guid.NewGuid();
            Name = name;
            IsDelete = false;
        }

        public Guid GradeId{get;set;}

        public string Name{get;set;}
        
        public bool IsDelete{get;set;}
        
        public DateTime CreateTime{get;set;}
        
        public Guid CreateBy{get;set;}
        
        public string CreateByName{get;set;}
        
        public DateTime UpdateTime{get;set;}
        
        public Guid UpdateBy{get;set;}
        
        public string UpdateByName{get;set;}
    }
}