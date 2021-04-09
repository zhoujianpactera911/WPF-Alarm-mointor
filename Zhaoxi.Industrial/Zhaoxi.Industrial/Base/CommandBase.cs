using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Zhaoxi.Industrial.Base
{
    public class CommandBase : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return DoCanExecute?.Invoke(parameter) == true;
        }

        public void Execute(object parameter)
        {
            DoExecute?.Invoke(parameter);
        }

        public Action<object> DoExecute { get; set; }
        public Func<object, bool> DoCanExecute { get; set; }

        public CommandBase() : this(null) { }

        public CommandBase(Action<object> execute) : this(execute, (o) => true) { }

        public CommandBase(Action<object> execute, Func<object, bool> can_execute)
        {
            this.DoExecute = execute;
            this.DoCanExecute = can_execute;
        }
    }
}
