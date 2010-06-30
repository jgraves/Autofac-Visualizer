using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace Graves.Visualizers.Autofac.UI {
	public class VisualAdorner : Adorner {
		readonly VisualCollection children;
		readonly FrameworkElement child;

		public VisualAdorner(FrameworkElement adornerElement, UIElement adornedElement)
			: base(adornedElement) {
			children = new VisualCollection(this);
			child = adornerElement;
			children.Add(child);
			AddLogicalChild(child);
		}

		//void AdornedElementMouseLeave(object sender, System.Windows.Input.MouseEventArgs e) {
		//  Visibility = Visibility.Hidden;
		//}

		//void AdornedElementMouseEnter(object sender, System.Windows.Input.MouseEventArgs e) {
		//  Visibility = Visibility.Visible;
		//}

		//protected override void OnRender(DrawingContext drawingContext) {
		//  var adornedElementRect = new Rect(AdornedElement.DesiredSize);

		//  // Some arbitrary drawing implements.
		//  var renderBrush = new SolidColorBrush(Colors.Green) { Opacity = 0.2 };
		//  var renderPen = new Pen(new SolidColorBrush(Colors.Navy), 1.5);
		//  var renderRadius = 5.0;

		//  // Draw a circle at each corner.
		//  drawingContext.DrawEllipse(renderBrush, renderPen, adornedElementRect.BottomRight, renderRadius, renderRadius);
		//}

		protected override Visual GetVisualChild(int index) {
			return children[index];
		}

		protected override int VisualChildrenCount {
			get {
				return children.Count;
			}
		}

		protected override Size MeasureOverride(Size constraint) {
			child.Measure(constraint);
			return AdornedElement.RenderSize;
		}

		protected override Size ArrangeOverride(Size finalSize) {
			var location = new Point(0, 0);
			var rect = new Rect(location, finalSize);
			child.Arrange(rect);
			return child.RenderSize;
		}
	}
}