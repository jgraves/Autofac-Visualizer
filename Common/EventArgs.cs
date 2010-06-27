using System;

namespace Graves.Visualizers.Autofac.Common {
	public class EventArgs<T> : EventArgs {
		public T Value { get; private set; }

		public EventArgs(T value) {
			Value = value;
		}
	}
}