using System;
using System.Windows.Input;

namespace DarumaBLLPortable.Commands
{
    public class RelayCommand: ICommand
    {
        private readonly Predicate<object> _canExcecute;
        private readonly Action<object> _command;

        //TODO: refactor command
        public RelayCommand(Action<object> command, Predicate<object> canExcecute = null)
        {
            _command = command;
            _canExcecute = canExcecute;
        }

        public bool CanExecute(object parameter)
        {
            if (_canExcecute == null)
            {
                return true;
            }
            return _canExcecute(parameter);
        }

        public void Execute(object parameter)
        {
            _command(parameter);
        }

        public event EventHandler CanExecuteChanged;

        public void RaiseCanExecuteChanged()
        {
            if (CanExecuteChanged != null)
            {
                CanExecuteChanged(this, EventArgs.Empty);
            }
        }
    }
}
