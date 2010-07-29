using System;
using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data.Interfaces {
	public interface IContainerInfo {
		IEnumerable<ComponentRegistration> GetServices();
		ResolutionTree GetBuildMap(Guid componentId);
	}
}