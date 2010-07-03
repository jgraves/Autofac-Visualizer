using System;

namespace AutofacVisualizer.Data.Structures {
	public class PreparingObjectEventArgs : EventArgs {
		public Type Type { get; private set; }

		public PreparingObjectEventArgs(Type type) {
			Type = type;
		}
	}
}