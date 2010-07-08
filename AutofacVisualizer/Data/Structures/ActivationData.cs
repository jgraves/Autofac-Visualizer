using System;
using System.Collections.Generic;
using Autofac.Core;

namespace AutofacVisualizer.Data.Structures {

	[Serializable]
	public struct ActivationData {
		public Type Built { get; set; }
		public IEnumerable<ActivationData> Buildees { get; set; }
	}
}