using System;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;

namespace BHCS.Domain.Models
{
    public class ClassRoom:AggregateRoot,ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public ClassRoom()
        {
        }

        public ClassRoom(string roomNo, string name, Guid classBuildingId)
        {
            Ensure.NotNullOrWhiteSpace(roomNo, "课室编号不能为空！");
            Ensure.NotNullOrWhiteSpace(name, "课室名不能为空！");
            Ensure.NotNull(classBuildingId, "所属教学楼不能为空！");

            RoomId = Guid.NewGuid();
            RoomNo = roomNo;
            Name = name;
            ClassBuildingId = classBuildingId;
            IsDelete = false;
        }

        public Guid RoomId{get;set;}

        public string RoomNo{get;set;}

        public string Name { get; set; }

        public Guid ClassBuildingId{get;set;}

        public DateTime CreateTime {get;set;}

        public Guid CreateBy{get;set;}

        public string CreateByName{get;set;}

        public DateTime UpdateTime{get;set;}
        
        public Guid UpdateBy{get;set;}
        
        public string UpdateByName{get;set;}
        
        public bool IsDelete{get;set;}
    }
}