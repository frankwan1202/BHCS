using System;
using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.FastDbCommon.Persistents.Model;

namespace BHCS.Domain.Models
{
    public class Classes:AggregateRoot,ICreateAudit,IUpdateAudit,ISoftDelete
    {
        public Classes()
        { }

        public Classes(string classesNo, string name, Guid gradeId)
        {
            Ensure.NotNullOrWhiteSpace(classesNo, "班级班号不能为空！");
            Ensure.NotNullOrWhiteSpace(name, "班级名称不能为空！");
            Ensure.NotNull(gradeId, "年级信息不能为空！");

            ClassesId = Guid.NewGuid();
            ClassesNo = classesNo;
            Name = name;
            GradeId = gradeId;
            IsDelete = false;
        }

        public Guid ClassesId{get;set;}

        public string ClassesNo{get;set;}

        public string Name{get;set;}

        [NotMapped]
        public Guid MajorId{get;set;}

        public Guid GradeId{get;set;}

        public DateTime CreateTime{get;set;}

        public Guid CreateBy{get;set;}

        public string CreateByName{get;set;}

        public DateTime UpdateTime{get;set;}

        public Guid UpdateBy{get;set;}

        public string UpdateByName{get;set;}

        public bool IsDelete{get;set;}
    }
}