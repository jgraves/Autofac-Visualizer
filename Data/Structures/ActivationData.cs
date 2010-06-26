using System;
using System.Collections.Generic;

namespace Graves.Visualizers.Autofac.Data.Structures {

	[Serializable]
	public struct ActivationData {
		public Type Built { get; set; }
		public IEnumerable<Type> Buildees { get; set; }
	}
}