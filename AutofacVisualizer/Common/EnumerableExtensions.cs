using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace AutofacVisualizer.Common {

	public static class EnumerableExtensions {
	
		public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection) {
			return new ObservableCollection<T>(collection);
		}

		public static ICollectionView ToView<T>(this IEnumerable<T> collection) {
			return CollectionViewSource.GetDefaultView(collection.ToObservable());
		}

		public static bool None<T>(this IEnumerable<T> collection) {
			return !collection.Any();
		}
		
		public static bool One<T>(this IEnumerable<T> collection) {
			return collection.Skip(1).Any();
		}
	}
}