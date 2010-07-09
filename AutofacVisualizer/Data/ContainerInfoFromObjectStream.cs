using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {
	public class ContainerInfoFromObjectStream : IContainerInfo {

		private readonly IObjectStream stream;

		public ContainerInfoFromObjectStream(IObjectStream stream) {
			this.stream = stream;
		}

		public IEnumerable<ServiceDefinition> GetServices() {
			return stream.GetObject<IEnumerable<ServiceDefinition>>();
		}

		public ActivationData GetBuildMap(ServiceDefinition item) {
			return stream.SendObject<ServiceDefinition, ActivationData>(item);
		}
	}
}