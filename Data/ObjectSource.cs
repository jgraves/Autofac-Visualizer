using System;
using System.Collections.Generic;
using Graves.Visualizers.Autofac.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Graves.Visualizers.Autofac.Data {
	
	public class ObjectSource {

		private readonly IVisualizerObjectProvider provider;

		public ObjectSource(IVisualizerObjectProvider provider) {
			this.provider = provider;
		}

		public IEnumerable<RegistrationPair> GetRegistrations() {
			return (IEnumerable<RegistrationPair>)provider.GetObject();
		}

		public IEnumerable<ActivationData> GetBuildMap(Type item) {
			return (IEnumerable<ActivationData>)provider.TransferObject(item);
		}
	}
}