using System;

namespace Graves.Visualizers.Autofac.Data.Structures {
	public class PreparingObjectEventArgs : EventArgs {
		public Type Type { get; private set; }

		public PreparingObjectEventArgs(Type type) {
			Type = type;
		}
	}
}