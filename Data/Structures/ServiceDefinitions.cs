using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;

namespace Graves.Visualizers.Autofac.Data.Structures {

	[Serializable]
	public class ServiceDefinitions : IEnumerable<ServiceDefinition> {

		private readonly IEnumerable<ServiceDefinition> services;

		public ServiceDefinitions(IEnumerable<IComponentRegistration> registrations) {

			var r = from componentRegistration in registrations
							from IServiceWithType service in componentRegistration.Services
							select new {
								service.ServiceType,
								componentRegistration.Activator.LimitType
							};

			services = (from reg in r
								 group reg.LimitType by reg.ServiceType into gs
								 select new ServiceDefinition {
									 RegisteredTypes = gs.ToList(),
									 ServiceType = gs.Key
								 }).ToList();
		}

		public IEnumerator<ServiceDefinition> GetEnumerator() {
			return services.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator() {
			return GetEnumerator();
		}
	}
}