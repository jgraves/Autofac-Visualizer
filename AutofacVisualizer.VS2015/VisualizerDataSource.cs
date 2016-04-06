using AutofacVisualizer.Data;
using Microsoft.VisualStudio.DebuggerVisualizers;
using System;
using System.IO;
using IContainer = Autofac.IContainer;

namespace AutofacVisualizer.VS2015
{

    public class VisualizerDataSource : VisualizerObjectSource {

		private ContainerRepository source;

		public override void TransferData(object target, Stream incomingData, Stream outgoingData) {
			object obj = Deserialize(incomingData);
			if (!(obj is Guid)) return;

			var componentId = (Guid)obj;
			var activations = source.GetBuildMap(componentId);
			Serialize(outgoingData, activations);
		}

		public override void GetData(object target, Stream outgoingData) {
			source = new ContainerRepository((IContainer)target);
			Serialize(outgoingData, source.GetComponents());
		}
	}
}