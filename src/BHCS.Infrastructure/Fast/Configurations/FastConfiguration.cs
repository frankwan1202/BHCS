using BHCS.Infrastructure.Fast.Commanding;
using BHCS.Infrastructure.Fast.Domain.UnitOfWorks;
using BHCS.Infrastructure.FastCommon.Components;
using BHCS.Infrastructure.FastCommon.Utilities;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Linq;
using BHCS.Infrastructure.Fast.Domain.Repositories;
using BHCS.Infrastructure.Fast.ThirdParty.FastDbCommon;

namespace BHCS.Infrastructure.Fast.Configurations
{
    public class FastConfiguration
    {
        private readonly FastConfigurationSetting _setting;

        public static FastConfiguration Instance;

        public FastConfiguration(FastConfigurationSetting setting=null)
        {
            _setting = setting ?? new FastConfigurationSetting();
            Instance = this;
        }

        public FastConfigurationSetting Setting => _setting;

        public FastConfiguration Intialize()
        {
            ObjectContainer.Register<IRepositoryContextManager, RepositoryContextManager>();
            ObjectContainer.Register<ICurrentRepositoryContextProvider, CurrentRepositoryContextProvider>();
            ObjectContainer.Register<ICommandBus, CommandBus>();
            ObjectContainer.Register<CommandHandler>();
            ObjectContainer.Register<CommandMetadataContainer>();
            ObjectContainer.Register<IRepositoryContext, FastDbCommonRepositoryContext>( LifeScope.Transient);
            return this;
        }

        public FastConfiguration RegisterAssembiles(params Assembly[] assembiles)
        {
            _setting.BussinessAssemblies = assembiles ?? throw new ArgumentNullException("Please register an assembly at least once!");
            ScanCommandMetadata(_setting.BussinessAssemblies);
            ObjectContainer.RegisterFromAssemblysForInterface(assembiles);
            return this;
        }

        private void ScanCommandMetadata(Assembly[] assembiles)
        {
            var metadataContainer = ObjectContainer.Resolve<CommandMetadataContainer>();
            var types = RefelectionHelper.GetImplInterfaceTypes(typeof(ICommandServicer), false, assembiles);
            foreach(var type in types)
            {
                foreach(var method in type.GetMethods().Where(p=>p.IsPublic&&p.GetParameters().Length==1))
                {
                    var metadata = new CommandMetadata(method.GetParameters()[0].ParameterType, type, method, method.ReturnType == typeof(void) ? false : true);
                    metadataContainer.AddMetadata(metadata);
                }
                ObjectContainer.Register(type,LifeScope.Transient);
            }
        }
    }
}
