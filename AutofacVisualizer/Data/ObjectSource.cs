using System.Collections.Generic;
using AutofacVisualizer.Data.Structures;

namespace AutofacVisualizer.Data {

	public class ObjectSource : IObjectSource {

	  private readonly ITalkToTheWire wireTalker;

	  public ObjectSource(ITalkToTheWire wireTalker)
	  {
	    this.wireTalker = wireTalker;
	  }

	  public IEnumerable<ServiceDefinition> GetRegistrations() {
	    return wireTalker.GetObject<IEnumerable<ServiceDefinition>>();
		}

		public ActivationData GetBuildMap(ServiceDefinition item) {
			return wireTalker.SendObject<ServiceDefinition, ActivationData>(item);
		}
	}
}