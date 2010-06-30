using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Graves.Visualizers.Autofac.UI.Core {
  public class BaseViewModel<T> : INotifyPropertyChanged {

    public event PropertyChangedEventHandler PropertyChanged;

    protected void NotifyPropertyChanged(Expression<Func<T, object>> exp) {
      if (exp.Body is MemberExpression) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)exp.Body).Member.Name));
      }
      else if (exp.Body is UnaryExpression) {
        if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs(((MemberExpression)((UnaryExpression)exp.Body).Operand).Member.Name));
      }
      else throw new ArgumentException(string.Format("Expressions of {0} type are not supported.", exp.Body.GetType().Name));
    }
  }
}