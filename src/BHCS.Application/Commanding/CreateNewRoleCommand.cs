using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class CreateNewRoleCommand:Command
    {
        public string RoleName { get; set; }

    }
}
