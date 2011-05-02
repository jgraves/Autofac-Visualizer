using System;
using System.Collections.Generic;

namespace AutofacVisualizer.ProfilingModule {
	public interface IContainerProfile
	{
		ComponentRegistrationInfo GetComponent(Guid id);
		IEnumerable<ComponentRegistrationInfo> Components { get; }
	}
}