using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;
using AutofacVisualizer.Common;

namespace AutofacVisualizer.UI.Controls {

    public class InterfacesConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var type = value as Type;
            return type != null ? type.GetInterfaces().Select(i => i.ToGenericTypeString()).ToFormattedString(", ") : null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}