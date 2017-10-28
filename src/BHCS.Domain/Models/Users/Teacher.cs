using BHCS.Infrastructure.Fast.Domain.Models;
using BHCS.Infrastructure.FastCommon.Utilities;
using BHCS.Infrastructure.FastDbCommon.Persistents.Model;
using System;
using System.Collections.Generic;

namespace BHCS.Domain.Models.Users
{
    public class Teacher:AggregateRoot
    {
        public Teacher()
        {
        }

        public Teacher(Guid majorId, User user)
        {
            Ensure.NotNull(majorId, "רҵ����Ϊ�գ�");
            Ensure.NotNull(user, "�û���Ϣ����Ϊ�գ�");
            Ensure.NotNull(user.UserId, "�û�ID����Ϊ�գ�");

            TeacherId = Guid.NewGuid();
            MajorId = majorId;
            UserId = user.UserId;
            User = user;
        }

        public Guid TeacherId{get;set;}

        public Guid UserId{get;set;}

        public Guid MajorId{get;set;}

        [NotMapped]
        public IList<Guid> CourseIds{get;set;}

        [NotMapped]
        public IList<Guid> ClassesIds{get;set;}

        [NotMapped]
        public User User { get; set; }
    }
}