using System;
using System.Collections.Generic;

namespace Graves.Visualizers.Autofac.Data.Structures {

	[Serializable]
	public class ServiceDefinition {
		public Type ServiceType { get; set; }
		public IEnumerable<Type> RegisteredTypes { get; set; }
	}
}