using System;
using System.IO;
using System.Linq;
using Autofac;
using Autofac.Core;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Graves.Visualizers.Autofac.Data {

	public class AutofacComponentRegistrySource : VisualizerObjectSource {
	
		private IContainer container;
		private IComponentRegistry registry;

		public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
			var data = Deserialize(incomingData) as Type;
			if (data == null) return;

			using (var tracker = new ResolutionTracker(registry.Registrations)) {
				object registration;
				container.TryResolve(data, out registration);
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