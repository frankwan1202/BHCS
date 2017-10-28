using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;

namespace BHCS.Domain.Models.Users
{
    public class Student:AggregateRoot
    {
        public Student()
        { }

        public Student(string studentNo, Guid classesId, Guid gradeId, Guid majorId, Guid userId)
        {
            Ensure.NotNullOrWhiteSpace(studentNo, "学号不能为空！");
            Ensure.NotNull(classesId, "班级信息不能为空！");
            Ensure.NotNull(gradeId, "年级信息不能为空！");
            Ensure.NotNull(majorId, "专业信息不能为空！");
            Ensure.NotNull(userId, "用户信息不能为空！");

            StudentNo = studentNo;
            ClassesId = classesId;
            GradeId = gradeId;
            MajorId = majorId;
            UserId = userId;
        }

        public Guid StudentId{get;set;}

        public string StudentNo{get;set;}

        public Guid ClassesId{get;set;}

        public Guid GradeId{get;set;}

        public Guid MajorId{get;set;}

        public Guid UserId{get;set;}
    }
}