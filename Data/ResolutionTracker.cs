using System;
using System.Collections.Generic;
using System.Linq;
using Graves.Visualizers.Autofac.Data.Structures;
using NGenerics.DataStructures.Trees;

namespace Graves.Visualizers.Autofac.Data {

	public class ResolutionTracker : IDisposable {

		private GeneralTree<Type> tree;
		private GeneralTree<Type> currentNode;

		private IEnumerable<IRegistration> Registrations { get; set; }

		public ResolutionTracker(IEnumerable<IRegistration> registrations) {
			Registrations = registrations;
			foreach (var r in Registrations) {
				r.Activating += OnActivating;
				r.Preparing += OnPreparing;
			}
		}

		
		public void Dispose() {
			foreach (var r in Registrations) {
				r.Activating -= OnActivating;
				r.Preparing -= OnPreparing;
			}
			foreach (var registration in Registrations) {
				registration.Dispose();
			}
		}

		public ActivationData Activations {
			get {
				return tree != null ? new ActivationData { Built = tree.Data, Buildees = FlattenTree(tree.ChildNodes).ToList() } : new ActivationData();
			}
		}

		public IEnumerable<ActivationData> FlattenTree(IEnumerable<GeneralTree<Type>> treeToFlatten) {
			foreach (var node in treeToFlatten) {
				var data = new ActivationData { Built = node.Data };

				if (!node.IsLeafNode) {
					data.Buildees = FlattenTree(node.ChildNodes).ToList();
				}

				yield return data;
			}
		}

		private void OnPreparing(object sender, PreparingObjectEventArgs e) {
			if (tree == null) {
				tree = new GeneralTree<Type>(e.Type);
				currentNode = tree;
			}
			else {
				var newNode = new GeneralTree<Type>(e.Type);
				currentNode.Add(newNode);
				currentNode = newNode;	
			}
		}

		private void OnActivating(object sender, ActivatingObjectEventArgs e) {
			if (e.Concrete == currentNode.Data) {
				currentNode = currentNode.Parent;
			}
		}
	}
}