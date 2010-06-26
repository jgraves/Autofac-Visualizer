using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Core;
using Graves.Visualizers.Autofac.Data;
using Graves.Visualizers.Autofac.Data.Structures;

namespace Graves.Visualizers.Autofac.UI {

	public class VisualizerViewModel : BaseViewModel<VisualizerViewModel> {

		private readonly ObjectSource objectSource;

		private ObservableCollection<ActivationData> buildMap;
		private ICollectionView types;

		public VisualizerViewModel(ObjectSource objectSource) {
			this.objectSource = objectSource;
			BuildCommand = new RelayCommand(o => Build(), o1 => Types.CurrentItem != null);
			BuildMap = new ObservableCollection<ActivationData>();

			RefreshTypes();
		}

		public ICommand BuildCommand { get; private set; }

		public ObservableCollection<ActivationData> BuildMap {
			get { return buildMap; }
			private set {
				buildMap = value;
				NotifyPropertyChanged(vm => vm.BuildMap);
			}
		}

		public ICollectionView Types {
			get { return types; }
			private set {
				types = value;
				NotifyPropertyChanged(vm => vm.Types);
			}
		}

		private void RefreshTypes() {
			Types = objectSource.GetRegistrations().Select(t => t.ServiceType).Distinct().ToView();
		}

		private void Build() {
			var item = Types.CurrentItem as Type;
			if (item == null) return;

			BuildMap = objectSource.GetBuildMap(item).ToObservable();
		}
	}
}