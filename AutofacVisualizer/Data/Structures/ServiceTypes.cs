using System;

namespace AutofacVisualizer.Data.Structures {
	[Serializable]
	public class TypedService {
		public Type Type { get; set; }
	}

	[Serializable]
	public class KeyedService : TypedService {
		public object Key { get; set; }
	}

	[Serializable]
	public class NamedService : TypedService {
		public string Name { get; set; }
	}
}