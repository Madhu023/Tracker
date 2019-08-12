using System;
using System.Windows.Input;

namespace Tracker.Common
{
    class RelayCommand : ICommand
    {
        readonly Action<object> _execute;

        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute) : this(execute, null) { }

        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            if (execute == null)
            {
                throw new ArgumentNullException("execute");
            }
            else
            {
                _execute = execute;
                _canExecute = canExecute;
            }
        }
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return (null == _canExecute) ? true : _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
