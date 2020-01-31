using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MVVMFramework
{
    public class FunctionCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;

        private Action<object> executeAction = null;
        private Predicate<object> executeCheck = null;

        public bool CanExecute(object parameter)
        {
            return executeCheck?.Invoke(parameter) ?? true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                executeAction?.Invoke(parameter);
            }
        }

        /// <summary>
        /// Instance of FunctionCommand
        /// </summary>
        /// <param name="function">The function to execute when Execute() is called</param>
        /// <param name="condition">The function to check whether Execute() is valid</param>
        public FunctionCommand(Action<object> function, Predicate<object> condition = null)
        {
            executeAction = function;
            executeCheck = condition;
        }
    }
}
