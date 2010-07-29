using System;
using System.Collections.Generic;
using AutofacVisualizer.Data.Interfaces;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {
	public class ContainerInfoStream : IContainerInfo {

		private readonly IObjectStream stream;

		public ContainerInfoStream(IObjectStream stream) {
			this.stream = stream;
		}

		public IEnumerable<ComponentRegistration> GetServices() {
			return stream.GetObject<IEnumerable<ComponentRegistration>>();
		}

		public ResolutionTree GetBuildMap(Guid componentId) {
			return stream.SendObject<Guid, ResolutionTree>(componentId);
		}
	}
}