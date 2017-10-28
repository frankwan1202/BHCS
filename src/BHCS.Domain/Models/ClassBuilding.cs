using System;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;

namespace BHCS.Domain.Models
{
    public class ClassBuilding:AggregateRoot,ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public ClassBuilding()
        { }

        public ClassBuilding(string name,string address)
        {
            Ensure.NotNullOrWhiteSpace(name, "教学楼名称不能为空！");
            Ensure.NotNullOrWhiteSpace(address, "教学楼地址不能为空！");

            BuildingId = Guid.NewGuid();
            Name = name;
            Address = address;
            IsDelete = false;
        }

        public Guid BuildingId{get;set;}

        public string Name{get;set;}

        public string Address{get;set;}
        
        public bool IsDelete{get;set;}
        
        public DateTime CreateTime{get;set;}
        
        public Guid CreateBy{get;set;}
        
        public string CreateByName{get;set;}
        
        public DateTime UpdateTime{get;set;}
        
        public Guid UpdateBy{get;set;}
        
        public string UpdateByName{get;set;}
    }
}