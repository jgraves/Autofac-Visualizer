using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {

    public class ObjectSource : IObjectSource {

        private readonly IBridgeDebuggerProcess wireBridge;

        public ObjectSource(IBridgeDebuggerProcess wireBridge) {
            this.wireBridge = wireBridge;
        }

        public IEnumerable<ServiceDefinition> GetRegistrations() {
            return wireBridge.GetObject<IEnumerable<ServiceDefinition>>();
        }

        public ActivationData GetBuildMap(ServiceDefinition item) {
            return wireBridge.SendObject<ServiceDefinition, ActivationData>(item);
        }
    }
}