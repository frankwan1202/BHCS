using BHCS.Infrastructure.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Querying.ReadModels
{
    public class StudentReadModel
    {
        public Guid StudentId { get; set; }

        public string StudentNo { get; set; }

        public string Account { get; set; }

        public string UserName { get; set; }

        public string MajorName { get; set; }

        public Guid MajorId { get; set; }

        public string ClassesName { get; set; }

        public Guid ClassesId { get; set; }

        public string GradeName { get; set; }

        public Guid GradeId { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

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
