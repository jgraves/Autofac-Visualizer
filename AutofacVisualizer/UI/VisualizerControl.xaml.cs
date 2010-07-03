namespace AutofacVisualizer.UI {

  public partial class VisualizerControl {

    public VisualizerControl(IVisualizerViewModel viewModel) {
      InitializeComponent();
      DataContext = viewModel;
    }
  }
}