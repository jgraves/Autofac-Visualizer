using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using Graves.Visualizers.Autofac.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Graves.Visualizers.Autofac.Data {

	public class AutofacComponentRegistrySource : VisualizerObjectSource {
	
		private IContainer container;
		private IComponentRegistry registry;

		public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
			var type = Deserialize(incomingData) as Type;
			if (type == null) return;

			var wrappedRegistrations = registry.Registrations.Select(r => new Registration(r));

			using (var tracker = new ResolutionTracker(type, wrappedRegistrations)) {
				object registration;
				container.TryResolve(type, out registration);
				Serialize(outgoingData, tracker.Activations.ToList());
			}
		}

		public override void GetData(object target, Stream outgoingData) {
			container = (IContainer)target;
			registry = container.ComponentRegistry;

			var registrations = from registration in registry.Registrations
			                    from service in registration.Services
			                    select new RegistrationPair {
			                    	RegisteredType = registration.Activator.LimitType,
			                    	ServiceType = ((IServiceWithType)service).ServiceType
			                    };

			Serialize(outgoingData, registrations.ToList());
		}
	}
}