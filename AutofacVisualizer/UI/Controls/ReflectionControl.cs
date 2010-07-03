using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace AutofacVisualizer.UI.Controls {
	public class ReflectionControl : Decorator {
		private readonly VisualBrush reflection;
		private readonly LinearGradientBrush opacityMask;
		private readonly ScaleTransform scaleTransform;

		public ReflectionControl() {
			opacityMask = new LinearGradientBrush {
				StartPoint = new Point(0, 0),
				EndPoint = new Point(0, 1),
				GradientStops = new GradientStopCollection{
					new GradientStop(Colors.White, 0),
					new GradientStop(Color.FromArgb(200, 255, 255, 255), .3),
					new GradientStop(Colors.Transparent, 1)
				}
			};

			reflection = new VisualBrush {
				Stretch = Stretch.None,
				TileMode = TileMode.None,
				Opacity = .4f,
				ViewboxUnits = BrushMappingMode.RelativeToBoundingBox,
				Viewbox = new Rect(0, .5, 1, .5)
			};

			scaleTransform = new ScaleTransform(1, -1);

		}

		protected override Size MeasureOverride(Size constraint) {
			// We only reflect half the control, so we need 1.5 times the space.
			if (Child == null) {
				return new Size(0, 0);
			}
			var newSize = new Size(constraint.Width, constraint.Height / 1.5);
			Child.Measure(newSize);
			return new Size(Child.DesiredSize.Width, Child.DesiredSize.Height * 1.5);
		}

		protected override Size ArrangeOverride(Size arrangeBounds) {
			if (Child == null) {
				return new Size(0, 0);
			}
			Child.Arrange(new Rect(0, 0, arrangeBounds.Width, arrangeBounds.Height / 1.5));
			return arrangeBounds;
		}

		protected override void OnRender(DrawingContext drawingContext) {
			// draw everything except the reflection
			base.OnRender(drawingContext);

			// set opacity
			drawingContext.PushOpacityMask(opacityMask);

			reflection.Visual = Child;
			var group = new TransformGroup();


			if (Child.RenderTransform is TransformGroup) {
				foreach (var child in ((TransformGroup)Child.RenderTransform).Children) {
					group.Children.Add(child);
				}
			}
			else {
				group.Children.Add(Child.RenderTransform);
			}

			scaleTransform.CenterY = (ActualHeight / 1.5) + ((ActualHeight / 1.5) / 4);
			scaleTransform.CenterX = ActualWidth / 2;
			group.Children.Add(scaleTransform);

			reflection.Transform = group;

			// draw the reflection
			drawingContext.DrawRectangle(
					reflection, null,
					new Rect(0, ActualHeight / 1.5, ActualWidth, ActualHeight / 3));

			// cleanup
			drawingContext.Pop();
		}
	}
}