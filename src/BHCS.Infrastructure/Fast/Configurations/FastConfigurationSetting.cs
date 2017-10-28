using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.Fast.Configurations
{
    public class FastConfigurationSetting
    {
        public Assembly[] BussinessAssemblies { get; set; }

        public string BussinessConnectString { get; set; }
    }
}
