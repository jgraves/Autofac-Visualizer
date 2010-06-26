namespace Graves.Visualizers.Autofac.UI {

	public partial class VisualizerControl {
		public VisualizerControl(VisualizerViewModel viewModel) {
			InitializeComponent();
			DataContext = viewModel;
		}
	}
}