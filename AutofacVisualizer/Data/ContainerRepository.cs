using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
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

		public ContainerRepository(IContainer container) {
			this.container = container;
			registry = container.ComponentRegistry;
		}

		public ResolutionTree GetBuildMap(Guid componentId) {

			var wrappedRegistrations = from reg in registry.Registrations
																 select (IComponentRegistrationListener)new ComponentRegistrationListener(reg);

			using (var tracker = new ResolutionTracker(wrappedRegistrations.ToList())) {
				var registration = registry.Registrations.First(r => r.Id == componentId);
				container.Resolve(registration, Enumerable.Empty<Parameter>());
				return tracker.Activations;
			}
		}

		public List<ComponentRegistration> GetComponents() {

			var components = from reg in registry.Registrations
											 select new ComponentRegistration {
												 Id = reg.Id,
												 Type = reg.Activator.LimitType,
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