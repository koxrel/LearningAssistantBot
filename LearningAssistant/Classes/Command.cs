using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace LearningAssistant
{
    public class Command : ICommand
    {
        private Action<object> _action;
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _action(parameter);
        }

        public Command(Action<object> action)
        {
            _action = action;
        }      
    }
}
