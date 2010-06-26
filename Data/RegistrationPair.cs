using System;

namespace Graves.Visualizers.Autofac.Data {

	[Serializable]
	public struct RegistrationPair {
		public Type RegisteredType { get; set; }
		public Type ServiceType { get; set; }
	}
}