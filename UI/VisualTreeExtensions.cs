using System.Windows.Media;

namespace Graves.Visualizers.Autofac.UI {
	static class VisualTreeExtensions {

		public static T FindDescendantOfType<T>(this Visual visual) where T : Visual {
			if (visual == null) return null;

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++) {
				var child = VisualTreeHelper.GetChild(visual, i) as Visual;

				if (child == null) continue;

				if (child is T) {
					return (T)child;
				}

				if (VisualTreeHelper.GetChildrenCount(child) > 0) {
					var childOfChild = child.FindDescendantOfType<T>();
					if (childOfChild != null) return childOfChild;
				}
			}

			return null;
		}
	}
}