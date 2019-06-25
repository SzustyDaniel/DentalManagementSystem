using System;
using System.Windows.Input;

namespace ClinicManager
{
    public class CustomCommand : ICommand
    {
        private readonly Predicate<object> _predicate;
        private readonly Action<object> _action;

        public CustomCommand(Predicate<object> predicate, Action<object> action)
        {
            _predicate = predicate ?? throw new ArgumentNullException(nameof(predicate));
            _action = action ?? throw new ArgumentNullException(nameof(action));
        }

        public bool CanExecute(object parameter)
        {
            return _predicate(parameter);
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public event EventHandler CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
    }
}
