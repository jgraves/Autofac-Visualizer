using System.Windows;
using Graves.Visualizers.Autofac.UI.BuildMap;
using HorizontalAlignment = System.Windows.HorizontalAlignment;

namespace Graves.Visualizers.Autofac.UI {

	public partial class VisualizerControl {

		public VisualizerControl(IVisualizerViewModel viewModel) {
			InitializeComponent();
			DataContext = viewModel;
			viewModel.ShowBuildMap += viewModel_ShowBuildMap;
		}

		void viewModel_ShowBuildMap(object sender, System.EventArgs e) {
			
			var window = new Window {
				Content =
					new BuildMapControl {
						HorizontalAlignment = HorizontalAlignment.Stretch,
						VerticalAlignment = VerticalAlignment.Stretch,
						DataContext = DataContext
					}
			};
			window.ShowDialog();
		}
	}
}