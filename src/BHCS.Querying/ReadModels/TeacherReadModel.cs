using BHCS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class TeacherReadModel
    {
        public Guid TeacherId { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string MajorName { get; set; }

        public Guid MajorId { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string RoleName { get; set; }

        public Guid RoleId { get; set; }

        public DateTime CreateTime { get; set; }

        public string CreateTimeStr => CreateTime.ToString("yyyy/MM/dd hh:mm:ss");

        public string CreateByName { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string UpdateTimeStr => UpdateTime.HasValue ? UpdateTime.Value.ToString("yyyy/MM/dd hh:mm:ss") : "";

        public string UpdateByName { get; set; }

        public AccountState State { get; set; }

        public string StateStr => State.ToText();
    }
}
