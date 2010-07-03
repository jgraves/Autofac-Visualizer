using System.ComponentModel;
using System.Windows.Input;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.UI {
	public interface IVisualizerViewModel : INotifyPropertyChanged {
		ICommand BuildCommand { get; }
		ActivationData BuildMap { get; }
		ICollectionView Services { get; }
		string FilterText { get; set; }
    View CurrentView { get; }
	}
}