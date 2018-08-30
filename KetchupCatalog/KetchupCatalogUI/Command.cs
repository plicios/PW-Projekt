using System;
using System.Windows.Input;

namespace Gorny.KetchupCatalog.KetchupCatalogUI
{
    class Command : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _canAction;
        public Command(Action<object> action, Predicate<object> canAction = null)
        {
            _action = action;
            _canAction = canAction;
        }
        public bool CanExecute(object parameter)
        {
            return _canAction?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                _action(parameter);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
