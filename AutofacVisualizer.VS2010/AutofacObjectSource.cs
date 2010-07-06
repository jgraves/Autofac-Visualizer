using System.IO;
using AutofacVisualizer.Data;
using AutofacVisualizer.Data.Structures;
using Microsoft.VisualStudio.DebuggerVisualizers;
using IContainer = Autofac.IContainer;

namespace AutofacVisualizer.VS2010 {

  public class AutofacObjectSource : VisualizerObjectSource {

    private AutofacData source;

    public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
      var service = Deserialize(incomingData) as ServiceDefinition;
      if (service == null) return;

      var activations = source.GetActivationData(service);
      Serialize(outgoingData, activations);
    }

    public override void GetData(object target, Stream outgoingData) {
      source = new AutofacData((IContainer)target);
      Serialize(outgoingData, source.GetServices());
    }
  }
}