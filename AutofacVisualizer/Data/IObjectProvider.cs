using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {
  public interface IObjectSource {
    IEnumerable<ServiceDefinition> GetRegistrations();
    ActivationData GetBuildMap(ServiceDefinition item);
  }

}