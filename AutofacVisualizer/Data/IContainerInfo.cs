using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {
	public interface IContainerInfo {
		IEnumerable<ServiceDefinition> GetServices();
		ActivationData GetBuildMap(ServiceDefinition item);
	}
}