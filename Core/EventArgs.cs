using System;

namespace Graves.Visualizers.Autofac.Core {
	public class EventArgs<T> : EventArgs {
		public T Value { get; private set; }

		public EventArgs(T value) {
			Value = value;
		}
	}
}