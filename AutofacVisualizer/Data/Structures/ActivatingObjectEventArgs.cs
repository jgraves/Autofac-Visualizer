using System;
using Autofac.Core;

namespace AutofacVisualizer.Data.Structures {
	public class ActivatingObjectEventArgs : EventArgs {
	    public Type Service { get; private set; }
		public Type Concrete { get; private set; }
		public IInstanceActivator Activator { get; private set; }

		public ActivatingObjectEventArgs(Type service, Type concrete, IInstanceActivator activator) {
		    Service = service;
			Concrete = concrete;
		    Activator = activator;
		}
	}
}