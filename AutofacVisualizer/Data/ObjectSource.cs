using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.Data {
	public interface IObjectSource {
		IEnumerable<ServiceDefinition> GetRegistrations();
		ActivationData GetBuildMap(ServiceDefinition item);
	}

	public class ObjectSource : IObjectSource {

		private readonly IVisualizerObjectProvider provider;

		public ObjectSource(IVisualizerObjectProvider provider) {
			this.provider = provider;
		}

		public IEnumerable<ServiceDefinition> GetRegistrations() {
			return (IEnumerable<ServiceDefinition>)provider.GetObject();
		}

		public ActivationData GetBuildMap(ServiceDefinition item) {
			return (ActivationData)provider.TransferObject(item);
		}
	}
}