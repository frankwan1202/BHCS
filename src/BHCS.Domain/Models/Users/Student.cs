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
            Ensure.NotNullOrWhiteSpace(studentNo, "ѧ�Ų���Ϊ�գ�");
            Ensure.NotNull(classesId, "�༶��Ϣ����Ϊ�գ�");
            Ensure.NotNull(gradeId, "�꼶��Ϣ����Ϊ�գ�");
            Ensure.NotNull(majorId, "רҵ��Ϣ����Ϊ�գ�");
            Ensure.NotNull(userId, "�û���Ϣ����Ϊ�գ�");

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