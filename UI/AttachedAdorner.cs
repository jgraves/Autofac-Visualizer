using System;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graves.Visualizers.Autofac.UI {
	public static class AttachedAdorner {

		public static Visual GetAdornWith(DependencyObject obj) {
			return (Visual)obj.GetValue(AdornWithProperty);
		}

		public static void SetAdornWith(DependencyObject obj, Visual value) {
			obj.SetValue(AdornWithProperty, value);
		}

		public static readonly DependencyProperty AdornWithProperty =
				DependencyProperty.RegisterAttached("AdornWith", typeof(Visual), typeof(AttachedAdorner), new UIPropertyMetadata(OnAdornWithChanged));

		private static void OnAdornWithChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
			var element = obj as UIElement;
			var adornWith = args.NewValue as FrameworkElement;
			if (element != null && adornWith != null) {
				//var constructor = newType.GetConstructor(new[] { typeof(UIElement) });
				//var adorner = (Adorner)constructor.Invoke(new[] { element });
				var layer = AdornerLayer.GetAdornerLayer(element);
				var adorner = new VisualAdorner(adornWith, element);
				layer.Add(adorner);
			}
		}
	}
}