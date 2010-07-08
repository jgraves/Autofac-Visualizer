using System;
using Autofac.Core;

namespace AutofacVisualizer.Data.Structures {

	public interface IRegistration : IDisposable {
		event EventHandler<ActivatingObjectEventArgs> Activating;
		event EventHandler<PreparingObjectEventArgs> Preparing;
	}

	public class Registration : IRegistration {
		private readonly IComponentRegistration registration;

		public event EventHandler<ActivatingObjectEventArgs> Activating;
		public event EventHandler<PreparingObjectEventArgs> Preparing;

		public Registration(IComponentRegistration registration) {
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