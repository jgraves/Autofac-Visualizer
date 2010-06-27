using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Common;
using Graves.Visualizers.Autofac.Data;
using Graves.Visualizers.Autofac.Data.Structures;
using QuickGraph;

namespace Graves.Visualizers.Autofac.UI {

	public class VisualizerViewModel : BaseViewModel<VisualizerViewModel>, IVisualizerViewModel {

		private readonly ObjectSource objectSource;

		private ActivationData buildMap;
		private ICollectionView services;

		public VisualizerViewModel(ObjectSource objectSource) {
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

		private void RefreshTypes() {
			Services = objectSource.GetRegistrations().ToView();
		}

		private void Build() {
			var item = Services.CurrentItem as ServiceDefinition;
			if (item == null) return;

			BuildMap = objectSource.GetBuildMap(item);
		}
	}
}