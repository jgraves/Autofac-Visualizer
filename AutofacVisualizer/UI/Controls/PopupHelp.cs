using System;
using System.Drawing;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AutofacVisualizer.UI.Controls {

	public class HelpTip {
		public ImageSource ImageSource { get; set; }
		public string Text { get; set; }
	}
	public class PopupHelp {

		public static string GetHelpTip(DependencyObject obj) {
			return (string)obj.GetValue(HelpTipProperty);
		}

		public static void SetHelpTip(DependencyObject obj, string value) {
			obj.SetValue(HelpTipProperty, value);
		}

		public static readonly DependencyProperty HelpTipProperty =
				DependencyProperty.RegisterAttached("HelpTip", typeof(string), typeof(PopupHelp), new UIPropertyMetadata("no help defined.", OnHelpTextChanged));

		private static void OnHelpTextChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args) {
			var hyperlink = obj as Hyperlink;
			if (hyperlink == null) throw new ArgumentException("This attached property is only valid on Hyperlinks.");

			var rd = new ResourceDictionary {
				Source = new Uri("pack://application:,,,/Graves.Visualizers.Autofac;component/Resources/Resources.xaml")
			};
			var style = (Style)rd["tooltipTemplate"];
			hyperlink.Style = style;

			((FrameworkElement)hyperlink.ToolTip).DataContext = new HelpTip {
				ImageSource =
					Imaging.CreateBitmapSourceFromHIcon(SystemIcons.Information.Handle, Int32Rect.Empty,
					                                    BitmapSizeOptions.FromEmptyOptions()),
				Text = (string)args.NewValue,
			};
		}
	}
}