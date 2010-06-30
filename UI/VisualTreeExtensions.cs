using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Graves.Visualizers.Autofac.UI {
	static class VisualTreeExtensions {

		public static T FirstWithValueFor<T>(this IEnumerable<T> objs, DependencyProperty prop) where T : DependencyObject {
			return objs.First(obj => obj.GetValue(prop) != prop.DefaultMetadata.DefaultValue);
		}

		public static T FindAncestorOfType<T>(this Visual visual) where T : Visual {
			if (visual == null) return null;

			var parent = VisualTreeHelper.GetParent(visual);

			while (parent != null) {
				if (parent is T) return (T)parent;	
			
				parent = VisualTreeHelper.GetParent(parent);
			}

			return null;
		}

		public static UIElement BranchThatContains(this UIElementCollection collection, UIElement me) {
			return collection.Cast<UIElement>().FirstOrDefault(me.IsDescendantOf);
		}

		public static IEnumerable<T> FindAllAncestorsOfType<T>(this Visual visual) where T : Visual {
			if (visual == null) yield break;

			var parent = VisualTreeHelper.GetParent(visual);

			while (parent != null) {
				if (parent is T) yield return (T)parent;	
			
				parent = VisualTreeHelper.GetParent(parent);
			}

			yield break;
		}

		public static DependencyObject FindAncestorWithValueFor(this DependencyObject visual, DependencyProperty property) {
			
			if (visual == null) return null;

			var parent = VisualTreeHelper.GetParent(visual);

			while (parent != null) {
				if (parent.ReadLocalValue(property) != DependencyProperty.UnsetValue) {
					return parent;
				}
			
				parent = VisualTreeHelper.GetParent(parent);
			}

			return null;
		}

		public static IEnumerable<DependencyObject> FindDescendantsWithPropertyAndValueOf(this DependencyObject visual, DependencyProperty property, object value) {

			var childCount = VisualTreeHelper.GetChildrenCount(visual);

			for (int i = 0; i < childCount; i++) {
				var child = VisualTreeHelper.GetChild(visual, i);
				
				if (Equals(child.GetValue(property), value)) {
					yield return child;
				}

				if (VisualTreeHelper.GetChildrenCount(child) < 1) continue;
				foreach (var childWithValue in child.FindDescendantsWithPropertyAndValueOf(property, value)) {
					yield return childWithValue;
				}
			}

			yield break;
		}

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