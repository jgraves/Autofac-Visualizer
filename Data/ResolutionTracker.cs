using System;
using System.Collections.Generic;
using System.Linq;
using Autofac.Core;

namespace Graves.Visualizers.Autofac.Data {

	public class ResolutionTracker : IDisposable {

		private readonly List<ActivatingEventArgs<object>> activatedRegistrations = new List<ActivatingEventArgs<object>>();
		private readonly List<PreparingEventArgs> preparingRegistrations = new List<PreparingEventArgs>();

		private IEnumerable<IComponentRegistration> Registrations { get; set; }

		public ResolutionTracker(IEnumerable<IComponentRegistration> registrations) {
			Registrations = registrations;
			foreach (var r in Registrations) {
				r.Activating += OnActivating;
				r.Preparing += OnPreparing;
			}
		}

		public void Dispose() {
			foreach (var r in Registrations) {
				r.Activating -= OnActivating;
				r.Preparing -= OnPreparing;
			}
		}

		public IEnumerable<ActivationData> Activations {
			get {
				return from argses in activatedRegistrations
							 select new ActivationData { Built = argses.Instance.GetType() };
			}
		}

		private void OnPreparing(object sender, PreparingEventArgs e) {
			Console.WriteLine(string.Format("Preparing {0}...", e.Component.Activator.LimitType));
			preparingRegistrations.Add(e);
		}

		private void OnActivating(object sender, ActivatingEventArgs<object> e) {
			Console.WriteLine(string.Format("Using {0} as {1}.", e.Instance.GetType().Name, e.Component.Activator.LimitType));
			activatedRegistrations.Add(e);
		}
	}
}