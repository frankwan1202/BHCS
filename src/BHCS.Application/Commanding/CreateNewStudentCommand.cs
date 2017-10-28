using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewStudentCommand:Command
    {
        public string Account { get; set; }

        public string StudentNo { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string Password { get; set; }

        public string RepeatPassword { get; set; }

        public Guid MajorId { get; set; }

        public Guid GradeId { get; set; }

        public Guid ClassesId { get; set; }
    }
}
