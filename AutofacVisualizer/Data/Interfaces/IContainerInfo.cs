using System;
using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {
	public interface IContainerInfo {
		IEnumerable<ComponentRegistration> GetServices();
		ResolutionTree GetBuildMap(Guid componentId);
	}
}