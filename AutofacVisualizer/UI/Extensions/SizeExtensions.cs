using System;
using System.Windows;

namespace AutofacVisualizer.UI.Extensions {
	public static class SizeExtensions {
		public static bool CanContain(this Size container, Size child) {
			return container.Width.ToLessPrecise() >= child.Width.ToLessPrecise() && container.Height.ToLessPrecise() >= child.Height.ToLessPrecise();
		}

		public static double ToLessPrecise(this double value) {
			return Math.Round(value, 2, MidpointRounding.AwayFromZero);
		}
	}
}