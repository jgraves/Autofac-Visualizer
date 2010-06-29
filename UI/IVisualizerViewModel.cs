using System.ComponentModel;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Data.Structures;

namespace Graves.Visualizers.Autofac.UI {
	public interface IVisualizerViewModel : INotifyPropertyChanged {
		ICommand BuildCommand { get; }
		ActivationData BuildMap { get; }
		ICollectionView Services { get; }
		string FilterText { get; set; }
		bool ShowDetails { get; }
	}
}