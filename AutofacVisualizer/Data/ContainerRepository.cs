using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {

	public class ContainerRepository {

		private readonly IContainer container;
		private readonly IComponentRegistry registry;

		public ContainerRepository(IContainer container) {
			this.container = container;
			this.registry = container.ComponentRegistry;
		}

		public ActivationData GetActivationData(ServiceDefinition service) {

			var wrappedRegistrations = from reg in registry.Registrations
																 select (IRegistration)new Registration(reg);

			using (var tracker = new ResolutionTracker(wrappedRegistrations.ToList())) {
				object registration;
				container.TryResolve(service.ServiceType, out registration);
				return tracker.Activations;
			}
		}

		public List<ServiceDefinition> GetServices() {
			var registrations = new ServiceDefinitions(registry.Registrations);
			return registrations.ToList();
		}
	}
}