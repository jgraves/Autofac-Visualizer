using System;

namespace Graves.Visualizers.Autofac.Data.Structures {
	public class ActivatingObjectEventArgs : EventArgs {
		public Type Service { get; private set; }
		public Type Concrete { get; private set; }

		public ActivatingObjectEventArgs(Type service, Type concrete) {
			Service = service;
			Concrete = concrete;
		}
	}
}