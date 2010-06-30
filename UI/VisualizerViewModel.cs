using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Common;
using Graves.Visualizers.Autofac.Data;
using Graves.Visualizers.Autofac.Data.Structures;

namespace Graves.Visualizers.Autofac.UI {

	public class VisualizerViewModel : BaseViewModel<VisualizerViewModel>, IVisualizerViewModel {

		private readonly IObjectSource objectSource;

		private ActivationData buildMap;
		private ICollectionView services;

		public VisualizerViewModel(IObjectSource objectSource) {
			this.objectSource = objectSource;
			BuildCommand = new RelayCommand(o => Build(), o1 => Services.CurrentItem != null);
			RefreshTypes();
		}

		public ICommand BuildCommand { get; private set; }

		public ActivationData BuildMap {
			get { return buildMap; }
			private set {
				buildMap = value;
				NotifyPropertyChanged(vm => vm.BuildMap);
			}
		}

		public ICollectionView Services {
			get { return services; }
			private set {
				services = value;
				NotifyPropertyChanged(vm => vm.Services);
			}
		}

		private string filterText = String.Empty;

		public string FilterText {
			get { return filterText; }
			set {
				filterText = value;
				Services.Filter =
					delegate(object o) {
            Func<Type, bool> contains = t => t.ToGenericTypeString().ToLower().Contains(value.ToLower());
						var definition = ((ServiceDefinition)o);
						return contains(definition.ServiceType) || definition.RegisteredTypes.Any(contains);
					};
				NotifyPropertyChanged(vm => vm.FilterText);
			}
		}

		private void RefreshTypes() {
			Services = objectSource.GetRegistrations().ToView();
		}

		public event EventHandler ShowBuildMap;

		private void Build() {
			var item = Services.CurrentItem as ServiceDefinition;
			if (item == null) return;

			BuildMap = objectSource.GetBuildMap(item);

			if (ShowBuildMap != null) ShowBuildMap(this, EventArgs.Empty);
		}
	}
}