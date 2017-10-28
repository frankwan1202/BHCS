using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.Components
{
    public interface IObjectContainer
    {
        void Register<TInterface, TService>(LifeScope lifeScope = LifeScope.Single);

        void Register<TService>(LifeScope lifeScope = LifeScope.Single);

        void Register(Type interfaceType, Type serviceType, LifeScope lifeScope = LifeScope.Single);

        void Register(Type serviceType, LifeScope lifeScope = LifeScope.Single);

        void RegisterFromAssemblysForInterface(params Assembly[] assemblys);

        void Register<TService>(TService instance, Type aliasType,LifeScope lifeScope= LifeScope.Single) where TService : class;

        T Resolve<T>();

        object Resolve(Type @type);
    }
}
