using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using AutofacContrib.Profiling;
using AutofacVisualizer.Data.Structures;
using ComponentRegistration = AutofacVisualizer.Data.Structures.ComponentRegistration;
using NamedService = AutofacVisualizer.Data.Structures.NamedService;
using TypedService = AutofacVisualizer.Data.Structures.TypedService;
using AutofacNamedService = Autofac.Core.NamedService;
using AutofacKeyedService = Autofac.Core.KeyedService;
using AutofacTypedService = Autofac.Core.TypedService;
using KeyedService = AutofacVisualizer.Data.Structures.KeyedService;

namespace AutofacVisualizer.Data {

	public class ContainerRepository {

		private readonly IContainer container;
		private readonly IComponentRegistry registry;
	    private readonly IContainerProfile _profile;

		public ContainerRepository(IContainer container) {
			this.container = container;
			registry = container.ComponentRegistry;
		    _profile = container.Resolve<IContainerProfile>();
		}

		public ResolutionTree GetBuildMap(Guid componentId) {
			var registration = registry.Registrations.Single(r => r.Id == componentId);

			if (!_profile.Components.Any(c => c.ComponentRegistration.Id == componentId))
                container.Resolve(registration, Enumerable.Empty<Parameter>());

            var componentInfo = _profile.GetComponent(componentId);

            return new ResolutionTree
            {
                Built = registration.Activator.LimitType,
                Buildees = componentInfo.Dependencies.Select(GetBuildMap)
            };
		}

		public List<ComponentRegistration> GetComponents() {

			var components =
                from reg in registry.Registrations
                let limitType = reg.Activator.LimitType
                where
                    limitType.Assembly != typeof(Container).Assembly &&
                    limitType.Assembly != typeof(ProfilingModule).Assembly
                select new ComponentRegistration
                {
				    Id = reg.Id,
				    Type = limitType,
				    Services =
					    (from IServiceWithType service in reg.Services
					    select GetService(service)).ToList()
			    };

			return components.ToList();
		}

		private TypedService GetService(IServiceWithType service) {
			if (service is AutofacNamedService) {
				return new NamedService {
					Name = ((AutofacNamedService)service).ServiceName,
					Type = ((AutofacNamedService)service).ServiceType
				};
			}
			if (service is AutofacKeyedService) {
				return new KeyedService {
					Key = ((AutofacKeyedService)service).ServiceKey,
					Type = ((AutofacKeyedService)service).ServiceType
				};
			}

			return new TypedService { Type = service.ServiceType };
		}
	}
}