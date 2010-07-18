using System;
using System.Collections.Generic;

namespace AutofacVisualizer.Data.Structures {

	[Serializable]
	public class ComponentRegistration {
		public Guid Id { get; set; }
		public Type Type { get; set; }
		public IEnumerable<TypedService> Services { get; set; }
	}
}