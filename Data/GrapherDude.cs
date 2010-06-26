using System;
using System.Collections.Generic;
using System.Linq;
using QuickGraph;
using QuickGraph.Algorithms.Observers;
using QuickGraph.Algorithms.Search;

namespace Graves.Visualizers.Autofac.Data {

	public class GrapherDude {
	
		private readonly IEnumerable<RegistrationPair> list;

		public GrapherDude(IEnumerable<RegistrationPair> list) {
			this.list = list;
		}

		public IDictionary<Type, Edge<Type>> Build() {
			var edges = from registrationPair in list
			            select new Edge<Type>(registrationPair.ServiceType, registrationPair.RegisteredType);

			var graph = edges.ToAdjacencyGraph<Type, Edge<Type>>();

			var algo = new DepthFirstSearchAlgorithm<Type, Edge<Type>>(graph);
			var observer = new VertexPredecessorRecorderObserver<Type, Edge<Type>>();
			using (observer.Attach(algo)) algo.Compute();
			return observer.VertexPredecessors;
		}
	}
}