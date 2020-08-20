using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace MVVMFramework
{
    public abstract class BuildCommand<T> : ICommand
    {
        protected abstract T Build(object parameter);

        public event EventHandler CanExecuteChanged;
        public event Action<T> BuildFinished;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            T builtObj = Build(parameter);
            BuildFinished?.Invoke(builtObj);
        }
    }
}
