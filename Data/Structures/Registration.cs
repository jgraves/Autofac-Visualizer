using System;
using Autofac.Core;

namespace Graves.Visualizers.Autofac.Data.Structures {
	public interface IRegistration {
		event EventHandler<ActivatingObjectEventArgs> Activating;
		event EventHandler<PreparingObjectEventArgs> Preparing;
	}

	public class Registration : IRegistration {
		private readonly IComponentRegistration registration;

		private event EventHandler<ActivatingObjectEventArgs> ActivatingShim = delegate { };
		private event EventHandler<PreparingObjectEventArgs> PreparingShim = delegate { };

		public event EventHandler<ActivatingObjectEventArgs> Activating {
			add {
				registration.Activating += OnActivating;
				ActivatingShim += value;
			}
			remove {
				registration.Activating -= OnActivating;
				ActivatingShim -= value;
			}
		}
		
		public event EventHandler<PreparingObjectEventArgs> Preparing {
			add {
				registration.Preparing += OnPreparing;
				PreparingShim += value;
			}
			remove {
				registration.Preparing -= OnPreparing;
				PreparingShim -= value;
			}
		}

		public Registration(IComponentRegistration registration) {
			this.registration = registration;
		}

		void OnPreparing(object sender, PreparingEventArgs e) {
			PreparingShim(this, new PreparingObjectEventArgs(e.Component.Activator.LimitType));
		}

		void OnActivating(object sender, ActivatingEventArgs<object> e) {
			ActivatingShim(this, new ActivatingObjectEventArgs(e.Instance.GetType(), e.Component.Activator.LimitType));
		}
	}
}