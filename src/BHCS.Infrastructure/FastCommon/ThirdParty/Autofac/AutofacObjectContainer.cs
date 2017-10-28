using Autofac;
using Autofac.Builder;
using BHCS.Infrastructure.FastCommon.Components;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BHCS.Infrastructure.FastCommon.ThirdParty.Autofac
{
    public class AutofacObjectContainer : IObjectContainer
    {
        private IContainer _container;

        public AutofacObjectContainer()
        {
            var builder = new ContainerBuilder();
            _container = builder.Build();
        }

        public void Register<TInterface, TService>(LifeScope lifeScope = LifeScope.Single)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TService>().As<TInterface>().SetLifeScope(lifeScope);
            builder.Update(_container);
        }

        public void Register<TService>(LifeScope lifeScope = LifeScope.Single)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<TService>().SetLifeScope(lifeScope);
            builder.Update(_container);
        }

        public void Register(Type interfaceType, Type serviceType, LifeScope lifeScope = LifeScope.Single)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(serviceType).As(interfaceType).SetLifeScope(lifeScope);
            builder.Update(_container);
        }

        public void Register(Type serviceType, LifeScope lifeScope = LifeScope.Single)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType(serviceType).SetLifeScope(lifeScope);
            builder.Update(_container);
        }

        public void Register<TService>(TService instance,Type aliasType, LifeScope lifeScope = LifeScope.Single) where TService:class
        {
            var builder = new ContainerBuilder();
            var registerBuilder = builder.RegisterInstance(instance).SetLifeScope(lifeScope);
            if (aliasType!=null) registerBuilder.As(aliasType);
            builder.Update(_container);
        }

        public void RegisterFromAssemblysForInterface(params Assembly[] assemblys)
        {
            var builder = new ContainerBuilder();
            builder.RegisterAssemblyTypes(assemblys).AsImplementedInterfaces();
            builder.Update(_container);
        }

        public T Resolve<T>()
        {
            return _container.Resolve<T>();
        }

        public object Resolve(Type type)
        {
            return _container.Resolve(type);
        }
    }

    public static class AutofacExetension
    {
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> SetLifeScope<TLimit, TActivatorData, TRegistrationStyle>(this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> registerInfo,LifeScope lifeScope)
        {
            if (lifeScope == LifeScope.Single) registerInfo.SingleInstance();
            return registerInfo;
        }
    }
}
