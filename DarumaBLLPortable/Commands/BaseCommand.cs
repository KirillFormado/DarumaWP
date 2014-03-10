using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DarumaBLLPortable.Commands
{
    class BaseCommand: ICommand
    {
        private bool _isEnabled;
        private readonly Action _command;

        public BaseCommand(Action command)
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
            _command();
        }

        public event EventHandler CanExecuteChanged;
    }
}
