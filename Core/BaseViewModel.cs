using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Graves.Visualizers.Autofac.Core {
	public class BaseViewModel<T> : INotifyPropertyChanged {

		public event PropertyChangedEventHandler PropertyChanged;

		protected void NotifyPropertyChanged(Expression<Func<T, object>> exp) {
			var expression = exp.Body as MemberExpression;
			if (expression != null)
				if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(expression.Member.Name));
		}
	}
}