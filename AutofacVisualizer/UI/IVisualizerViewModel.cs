using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.UI {
	public interface IVisualizerViewModel : INotifyPropertyChanged {
		ICommand BuildCommand { get; }
		IEnumerable<ResolutionTree> BuildMap { get; }
		ICollectionView Components { get; }
		string FilterText { get; set; }
    View CurrentView { get; }
	}
}