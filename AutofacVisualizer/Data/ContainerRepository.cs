using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Activators.Delegate;
using Autofac.Core.Activators.ProvidedInstance;
using Autofac.Core.Activators.Reflection;
using Autofac.Core.Lifetime;
using AutofacVisualizer.Data.Structures;
using AutofacVisualizer.ProfilingModule;
using ComponentRegistration = AutofacVisualizer.Data.Structures.ComponentRegistration;
using TypedService = AutofacVisualizer.Data.Structures.TypedService;
using KeyedService = AutofacVisualizer.Data.Structures.KeyedService;
using AutofacKeyedService = Autofac.Core.KeyedService;

namespace AutofacVisualizer.Data
{

	public class ContainerRepository
	{

		private readonly IContainer _container;
		private readonly IContainerProfile _profile;

		public ContainerRepository(IContainer container)
		{
			_container = container;
			_profile = container.Resolve<IContainerProfile>();
		}

		public ResolutionTree GetBuildMap(Guid componentId)
		{
			var componentInfo = _profile.GetComponent(componentId);

			IEnumerable<Guid> dependencies;
			if (!componentInfo.TryGetDependencies(out dependencies))
			{
				_container.ResolveComponent(componentInfo.ComponentRegistration, Enumerable.Empty<Parameter>());
			}

			componentInfo.TryGetDependencies(out dependencies);

			return new ResolutionTree
			{
				Built = BuildComponentRegistration(componentInfo.ComponentRegistration, componentInfo.ComponentRegistration.Activator.LimitType),
				Buildees = dependencies.Select(GetBuildMap).ToArray()
			};
		}

		public List<ComponentRegistration> GetComponents()
		{

            var components =

			  from info in _profile.Components
			  let reg = info.ComponentRegistration
			  let limitType = reg.Activator.LimitType

			  where

				  limitType.Assembly != typeof(Container).Assembly &&
				  limitType.Assembly != typeof(ProfilingModule.ProfilingModule).Assembly

			  select BuildComponentRegistration(reg, limitType, info);

			return components.ToList();
		}

		private ComponentRegistration BuildComponentRegistration(IComponentRegistration reg, Type limitType)
		{
			return new ComponentRegistration
			{
				Id = reg.Id,
				Type = limitType,
				Services = reg.Services.OfType<IServiceWithType>().Select(GetService).ToList(),
				InstanceScope = GetInstanceScope(reg),
				ActivatorType = GetActivatorType(reg),
				ActivatorDescription = GetActivatorDescription(reg)
			};
		}

		private ComponentRegistration BuildComponentRegistration(IComponentRegistration reg, Type limitType, ComponentRegistrationInfo info)
		{
			return new ComponentRegistration
			{
				Id = reg.Id,
				Type = limitType,
				Services = reg.Services.OfType<IServiceWithType>().Select(GetService).ToList(),
				InstanceScope = GetInstanceScope(reg),
				ActivationCount = info.ActivationCount,
				ActivatorType = GetActivatorType(reg),
				ActivatorDescription = GetActivatorDescription(reg)
			};
		}

		static string GetActivatorDescription(IComponentRegistration reg)
		{
			var activator = GetActivator(reg);
			return activator.ToString();
		}

		static IInstanceActivator GetActivator(IComponentRegistration reg)
		{
			var profiling = reg.Activator as ProfilingActivator;
			return profiling != null ? profiling.InnerActivator : reg.Activator;
		}

		static ActivatorType GetActivatorType(IComponentRegistration reg)
		{
			var activator = GetActivator(reg);
			if (activator is ReflectionActivator)
				return ActivatorType.Reflection;
			if (activator is ProvidedInstanceActivator)
				return ActivatorType.ProvidedInstance;
			if (activator is DelegateActivator)
				return ActivatorType.Delegate;
			return ActivatorType.Unknown;
		}

		static InstanceScope GetInstanceScope(IComponentRegistration reg)
		{
			if (reg.Sharing == InstanceSharing.Shared)
			{
				if (reg.Lifetime is RootScopeLifetime)
					return InstanceScope.SingleInstance;
				if (reg.Lifetime is MatchingScopeLifetime)
					return InstanceScope.PerMatchingLifetimeScope;
				if (reg.Lifetime is CurrentScopeLifetime)
					return InstanceScope.PerLifetimeScope;
			}
			else if (reg.Sharing == InstanceSharing.None)
			{
				if (reg.Lifetime is CurrentScopeLifetime)
					return InstanceScope.PerDependency;
			}

			return InstanceScope.Unknown;
		}

		private static TypedService GetService(IServiceWithType service)
		{
			if (service is AutofacKeyedService)
			{
				return new KeyedService
				{
					Key = ((AutofacKeyedService)service).ServiceKey,
					Type = ((AutofacKeyedService)service).ServiceType
				};
			}

			return new TypedService { Type = service.ServiceType };
		}
	}
}