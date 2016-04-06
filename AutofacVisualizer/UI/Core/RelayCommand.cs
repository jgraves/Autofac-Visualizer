using System;
using System.Windows.Input;

namespace AutofacVisualizer.UI.Core {
    public class RelayCommand : ICommand {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute) {
            this.execute = execute;
            this.canExecute = canExecute;
        }

        public void Execute(object parameter) {
            execute(parameter);
        }

        public bool CanExecute(object parameter) {
            return canExecute(parameter);
        }

        public event EventHandler CanExecuteChanged {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
    }
}