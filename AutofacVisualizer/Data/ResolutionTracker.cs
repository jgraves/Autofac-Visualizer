using System;
using System.Collections.Generic;
using System.Linq;
using AutofacVisualizer.Data.Structures;
using NGenerics.DataStructures.Trees;

namespace AutofacVisualizer.Data {

	[Serializable]
	public struct ResolutionTree {
		public Type Built { get; set; }
		public IEnumerable<ResolutionTree> Buildees { get; set; }
	}

	public class ResolutionTracker : IDisposable {

		private GeneralTree<Type> tree;
		private GeneralTree<Type> currentNode;

		private IEnumerable<IComponentRegistrationListener> Registrations { get; set; }

		public ResolutionTracker(IEnumerable<IComponentRegistrationListener> registrations) {
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

		public ResolutionTree Activations {
			get {
				return tree != null ? new ResolutionTree { Built = tree.Data, Buildees = FlattenTree(tree.ChildNodes).ToList() } : new ResolutionTree();
			}
		}

		public IEnumerable<ResolutionTree> FlattenTree(IEnumerable<GeneralTree<Type>> treeToFlatten) {
			foreach (var node in treeToFlatten) {
				var data = new ResolutionTree { Built = node.Data };

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