using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastCommon.Logging;
using BHCS.Infrastructure.FastCommon.Serializes;
using BHCS.Infrastructure.FastCommon.ThirdParty.Autofac;
using BHCS.Infrastructure.FastCommon.ThirdParty.Newtonsoft;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BHCS.Infrastructure.FastCommon.Configurations
{
    public class Configuration
    {
        public static readonly Configuration Instance;

        static Configuration()
        {
            Instance = new Configuration();
        }
        

        public Configuration UseAutofac()
        {
            ObjectContainer.SetContainer(new AutofacObjectContainer());
            return this;
        }

        public Configuration RegisterComponents()
        {
            ObjectContainer.Register<ILoggerFactory, NullLoggerFactory>();
            ObjectContainer.Register<IUserSession, NullUserSession>();
            return this;
        }

        public Configuration UseNewtonsoftJson()
        {
            ObjectContainer.Register<IJsonSerializer, NewtonsoftJsonSerializer>();
            return this;
        }

        public Configuration UseUserSession(IUserSession userSession)
        {
            ObjectContainer.RegisterInstance(userSession);
            return this;
        }
    }
}
