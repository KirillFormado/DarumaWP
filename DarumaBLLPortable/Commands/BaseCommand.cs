using System;
using System.Windows.Input;

namespace DarumaBLLPortable.Commands
{
    public class BaseCommand: ICommand
    {
        private bool _isEnabled;
        private readonly Action<object> _command;

        public BaseCommand(Action<object> command)
        {
            _command = command;
        }

        public bool IsEnabled
        {
            get { return _isEnabled; }
            set
            {
                if (value != _isEnabled)
                {
                    IsEnabled = value;
                    if (CanExecuteChanged != null)
                    {
                        CanExecuteChanged(this, EventArgs.Empty);
                    }
                }
            }
        }

        public bool CanExecute(object parameter)
        {
            return IsEnabled;
        }

        public void Execute(object parameter)
        {
            _command(parameter);
        }

        public event EventHandler CanExecuteChanged;
    }
}
