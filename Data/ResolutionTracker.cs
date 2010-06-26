using System;
using System.Collections.Generic;
using System.Linq;
using Graves.Visualizers.Autofac.Data.Structures;
using NGenerics.DataStructures.Trees;

namespace Graves.Visualizers.Autofac.Data {

	public class ResolutionTracker : IDisposable {

		private readonly GeneralTree<Type> tree;
		private GeneralTree<Type> currentNode;

		private IEnumerable<IRegistration> Registrations { get; set; }

		public ResolutionTracker(Type typeBeingBuilt, IEnumerable<IRegistration> registrations) {
			Registrations = registrations;
			foreach (var r in Registrations) {
				r.Activating += OnActivating;
				r.Preparing += OnPreparing;
			}

			tree = new GeneralTree<Type>(typeBeingBuilt);
			currentNode = tree;
		}

		public void Dispose() {
			foreach (var r in Registrations) {
				r.Activating -= OnActivating;
				r.Preparing -= OnPreparing;
			}
		}

		public IEnumerable<ActivationData> Activations {
			get {
				return from node in FlattenTree(tree) select node;
			}
		}

		public IEnumerable<ActivationData> FlattenTree(GeneralTree<Type> treeToFlatten) {
			foreach (var node in treeToFlatten.ChildNodes) {
				if (!node.IsLeafNode) {
					foreach (var childNode in FlattenTree(node)) {
						yield return childNode;
					}
				}
				yield return new ActivationData { Built = node.Data, Buildees = node.ChildNodes.Select(n => n.Data).ToList() };
			}
		}
		private void OnPreparing(object sender, PreparingObjectEventArgs e) {
			Console.WriteLine(string.Format("Preparing {0}.", e.Type));

			var newNode = new GeneralTree<Type>(e.Type);
			currentNode.Add(newNode);
			currentNode = newNode;
		}

		private void OnActivating(object sender, ActivatingObjectEventArgs e) {
			Console.WriteLine(string.Format("Activating {0} as {1}.", e.Concrete, e.Service));
			
			if (e.Concrete == currentNode.Data) {
				currentNode = currentNode.Parent;
			}
		}
	}
}