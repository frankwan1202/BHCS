using BHCS.Infrastructure.Fast.Commanding;
using System;
using System.Collections.Generic;
using System.Text;

namespace BHCS.Application.Commanding
{
    public class LoginCommand:Command
    {
        public string Account { get; set; }

        public string Password { get; set; }
    }
}
