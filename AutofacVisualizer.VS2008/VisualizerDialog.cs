using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using AutofacVisualizer.Data;
using AutofacVisualizer.UI;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.VS2008 {

  public class VisualizerDialog : DialogDebuggerVisualizer {

    protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
      System.Windows.Application.ResourceAssembly = Assembly.GetExecutingAssembly();


      var objectSource = new ContainerInfoFromObjectStream(new VisualizerObjectStream(objectProvider));
      var viewModel = new VisualizerViewModel(objectSource);
      var child = new VisualizerControl(viewModel);

      var host = new ElementHost { Dock = DockStyle.Fill, MinimumSize = new Size(600, 600) };
      host.Child = child;

      windowService.ShowDialog(host);
    }

    public static void TestShowVisualizer(object objectToVisualize) {
      var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(VisualizerDialog),
                                                         typeof(VisualizerDataSource));
      visualizerHost.ShowVisualizer();
    }
  }
}