using System.IO;
using Autofac;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace AutofacVisualizer.VS2008 {

  public class VisualizerDataSource : VisualizerObjectSource {

    private ContainerRepository source;

    public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
      var service = Deserialize(incomingData) as ServiceDefinition;
      if (service == null) return;

      var activations = source.GetBuildMap(service);
      Serialize(outgoingData, activations);
    }

    public override void GetData(object target, Stream outgoingData) {
      source = new ContainerRepository((IContainer)target);
      Serialize(outgoingData, source.GetServices());
    }
  }
}