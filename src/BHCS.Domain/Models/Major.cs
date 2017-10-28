using System;
using BHCS.Infrastructure.Fast.Domain.Models;

namespace BHCS.Domain.Models
{
    public class Major:ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public Guid MajorId{get;set;}

        public string Name{get;set;}

        public bool IsEnable{get;set;}

        public DateTime CreateTime{get;set;}

        public Guid CreateBy{get;set;}

        public string CreateByName{get;set;}

        public DateTime UpdateTime{get;set;}

        public Guid UpdateBy{get;set;}

        public string UpdateByName{get;set;}

        public bool IsDelete{get;set;}
    }
}