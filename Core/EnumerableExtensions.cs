using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;

namespace Graves.Visualizers.Autofac.Core {

	public static class EnumerableExtensions {
	
		public static ObservableCollection<T> ToObservable<T>(this IEnumerable<T> collection) {
			return new ObservableCollection<T>(collection);
		}

		public static ICollectionView ToView<T>(this IEnumerable<T> collection) {
			return CollectionViewSource.GetDefaultView(collection.ToObservable());
		}
	}
}