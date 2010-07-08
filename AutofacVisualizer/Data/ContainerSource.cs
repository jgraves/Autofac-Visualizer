using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {

	public interface IContainerSource {
		IEnumerable<ServiceDefinition> GetRegistrations();
		ActivationData GetBuildMap(ServiceDefinition item);
	}

	public class ContainerSource : IContainerSource {

		private readonly IObjectProvider provider;

		public ContainerSource(IObjectProvider provider) {
			this.provider = provider;
		}

		public IEnumerable<ServiceDefinition> GetRegistrations() {
			return provider.GetObject<IEnumerable<ServiceDefinition>>();
		}

		public ActivationData GetBuildMap(ServiceDefinition item) {
			return provider.SendObject<ServiceDefinition, ActivationData>(item);
		}
	}
}