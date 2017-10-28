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
            Ensure.NotNullOrWhiteSpace(roomNo, "���ұ�Ų���Ϊ�գ�");
            Ensure.NotNullOrWhiteSpace(name, "����������Ϊ�գ�");
            Ensure.NotNull(classBuildingId, "������ѧ¥����Ϊ�գ�");

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