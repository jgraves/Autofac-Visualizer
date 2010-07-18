using System;
using Autofac.Core;

namespace AutofacVisualizer.Data.Structures {

	public class PreparingObjectEventArgs : EventArgs {
		public Type Type { get; private set; }

		public PreparingObjectEventArgs(Type type) {
			Type = type;
		}
	}

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

	public interface IComponentRegistrationListener : IDisposable {
		event EventHandler<ActivatingObjectEventArgs> Activating;
		event EventHandler<PreparingObjectEventArgs> Preparing;
	}

	public class ComponentRegistrationListener : IComponentRegistrationListener {
		private readonly IComponentRegistration registration;

		public event EventHandler<ActivatingObjectEventArgs> Activating;
		public event EventHandler<PreparingObjectEventArgs> Preparing;

		public ComponentRegistrationListener(IComponentRegistration registration) {
			this.registration = registration;
			registration.Activating += OnActivating;
			registration.Preparing += OnPreparing;
		}

		void OnPreparing(object sender, PreparingEventArgs e) {
			if (Preparing != null) {
				Preparing(this, new PreparingObjectEventArgs(e.Component.Activator.LimitType));
			}
		}

		void OnActivating(object sender, ActivatingEventArgs<object> e) {
			if (Activating != null) {
				Activating(this, new ActivatingObjectEventArgs(e.Instance.GetType(), e.Component.Activator.LimitType, e.Component.Activator));
			}
		}

		public void Dispose() {
			registration.Preparing -= OnPreparing;
			registration.Activating -= OnActivating;
		}
	}
}