using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using Graves.Visualizers.Autofac.Data.Structures;
using QuickGraph;

namespace Graves.Visualizers.Autofac.UI {
	public interface IVisualizerViewModel {
		ICommand BuildCommand { get; }
		ActivationData BuildMap { get; }
		ICollectionView Services { get; }
		event PropertyChangedEventHandler PropertyChanged;
	}
}