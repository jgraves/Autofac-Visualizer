using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using Graves.Visualizers.Autofac.UI;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace Graves.Visualizers.Autofac.Data {

  public class AutofacVisualizer : DialogDebuggerVisualizer {

    protected override void Show(IDialogVisualizerService windowService, IVisualizerObjectProvider objectProvider) {
      System.Windows.Application.ResourceAssembly = Assembly.GetExecutingAssembly();

      var objectSource = new ObjectSource(objectProvider);

      //var dude = new GrapherDude(list);
      //var nodes = dude.Build();
      //var tree = new TreeBuilder(nodes);

      var viewModel = new VisualizerViewModel(objectSource);
      var child = new VisualizerControl(viewModel);

      var host = new ElementHost { Dock = DockStyle.Fill, MinimumSize = new Size(600, 600) };
      host.Child = child;

      windowService.ShowDialog(host);
    }

    public static void TestShowVisualizer(object objectToVisualize) {
      var visualizerHost = new VisualizerDevelopmentHost(objectToVisualize, typeof(AutofacVisualizer),
                                                         typeof(AutofacComponentRegistrySource));
      visualizerHost.ShowVisualizer();
    }
  }
}