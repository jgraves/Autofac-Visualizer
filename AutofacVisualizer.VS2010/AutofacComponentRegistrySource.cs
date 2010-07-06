using System.ComponentModel;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;
using IContainer = Autofac.IContainer;

namespace AutofacVisualizer.VS2010 {

	public class AutofacComponentRegistrySource : VisualizerObjectSource {

		private IContainer container;
		private IComponentRegistry registry;

		public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
			var service = Deserialize(incomingData) as ServiceDefinition;
			if (service == null) return;

			var wrappedRegistrations = registry.Registrations.Select(r => (IRegistration)new Registration(r)).ToList();

			using (var tracker = new ResolutionTracker(wrappedRegistrations)) {
				object registration;
				container.TryResolve(service.ServiceType, out registration);
				Serialize(outgoingData, tracker.Activations);
			}
		}

		public override void GetData(object target, Stream outgoingData) {
			container = (IContainer)target;
			registry = container.ComponentRegistry;
			var registrations = new ServiceDefinitions(registry.Registrations);
			Serialize(outgoingData, registrations.ToList());
		}
	}
}