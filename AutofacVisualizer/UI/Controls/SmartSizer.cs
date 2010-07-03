using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using AutofacVisualizer.Common;
using AutofacVisualizer.UI.Extensions;
using NGenerics.Extensions;

namespace AutofacVisualizer.UI.Controls {

	public class IsDetailChangedArgs : RoutedEventArgs {
		public IsDetailChangedArgs(object source, bool isDetailShown)
			: base(SmartSizer.IsDetailChangedEvent, source) {
			IsDetailShown = isDetailShown;
		}

		public bool IsDetailShown { get; private set; }
	}

	public static class SmartSizer {

		public delegate void IsDetailChangedHandler(object sender, IsDetailChangedArgs e);

		public static readonly RoutedEvent IsDetailChangedEvent = EventManager.RegisterRoutedEvent("IsDetailChanged",
																																									RoutingStrategy.Tunnel,
																																									typeof(IsDetailChangedHandler),
																																									typeof(SmartSizer));
		

		public static void AddIsDetailChangedHandler (DependencyObject obj, IsDetailChangedHandler handler) {
			var element = obj as UIElement;
			if (element != null) {
				element.AddHandler(IsDetailChangedEvent, handler);
			}
		}

		public static void RemoveIsDetailChangedHandler (DependencyObject obj, IsDetailChangedHandler handler) {
			var element = obj as UIElement;
			if (element != null) {
				element.RemoveHandler(IsDetailChangedEvent, handler);
			}
		}

		public static bool GetSizeSmartly(DependencyObject obj) {
			return (bool)obj.GetValue(SizeSmartlyProperty);
		}

		public static void SetSizeSmartly(DependencyObject obj, bool value) {
			obj.SetValue(SizeSmartlyProperty, value);
		}

		// Using a DependencyProperty as the backing store for SizeSmartly.  This enables animation, styling, binding, etc...
		public static readonly DependencyProperty SizeSmartlyProperty =
				DependencyProperty.RegisterAttached("SizeSmartly", typeof(bool), typeof(SmartSizer), new UIPropertyMetadata(false, OnSizeSmartlyChanged));

		private static void OnSizeSmartlyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
			var panel = obj as Panel;
			if (panel != null) {
				panel.SizeChanged += PanelSizeChanged;
			}
		}

		private static bool currentlySizing;

		static void PanelSizeChanged(object sender, SizeChangedEventArgs e) {
			var panel = sender as Panel;
			if (panel != null) {
				if (currentlySizing) return;
				currentlySizing = true;

				var renderIsBigEnough = panel.Children.Cast<UIElement>().All(
					delegate(UIElement child) {
						detailElements[child].Visibility = Visibility.Visible;
						child.Measure(new Size(double.PositiveInfinity, double.PositiveInfinity));
						return child.RenderSize.CanContain(child.DesiredSize);
					});

				if (!renderIsBigEnough) {
					detailElements.Values.ForEach(fe => fe.Visibility = Visibility.Collapsed);
					panel.RaiseEvent(new IsDetailChangedArgs(panel, false));
				}
				else {
					detailElements.Values.ForEach(fe => fe.Visibility = Visibility.Visible);
					panel.RaiseEvent(new IsDetailChangedArgs(panel, true));
				}
				currentlySizing = false;
			}
		}

		private static readonly Dictionary<UIElement, UIElement> detailElements =
																							new Dictionary<UIElement, UIElement>();

		public static bool GetIsDetail(DependencyObject obj) {
			return (bool)obj.GetValue(IsDetailProperty);
		}

		public static void SetIsDetail(DependencyObject obj, bool value) {
			obj.SetValue(IsDetailProperty, value);
		}

		public static readonly DependencyProperty IsDetailProperty =
				DependencyProperty.RegisterAttached("IsDetail", typeof(bool), typeof(SmartSizer), new UIPropertyMetadata(false, OnIsDetailChanged));

		private static bool isDetailChanging;

		private static void OnIsDetailChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
			var element = obj as FrameworkElement;
			var isDetail = (bool)args.NewValue;
			if (element == null) return;
			if (isDetailChanging) return;

			isDetailChanging = true;

			var container = element.FindAllAncestorsOfType<Panel>()
				.FirstWithValueFor(SizeSmartlyProperty);
			var panelChild = container.Children.BranchThatContains(element);

			if (isDetail) {
				detailElements.SafeAdd(panelChild, element);
			}
			else {
				detailElements.SafeRemove(panelChild);
			}

			isDetailChanging = false;
		}
	}
}