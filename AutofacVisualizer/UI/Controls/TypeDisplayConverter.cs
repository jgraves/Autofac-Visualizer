using System;
using System.Globalization;
using System.Windows.Data;
using AutofacVisualizer.Common;

namespace AutofacVisualizer.UI.Controls {
    public class TypeDisplayConverter : IValueConverter {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
            var type = value as Type;
            return type == null ? value : type.ToGenericTypeString();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
            throw new NotImplementedException();
        }
    }
}