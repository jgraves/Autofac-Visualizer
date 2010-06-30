using System.Windows;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace Graves.Visualizers.Autofac.UI {

  public partial class VisualizerControl {

    public VisualizerControl(IVisualizerViewModel viewModel) {
      InitializeComponent();
      DataContext = viewModel;
    }
  }
}