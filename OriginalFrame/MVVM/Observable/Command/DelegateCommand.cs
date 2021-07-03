using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class DelegateCommand : ICommand
    {
        public Action<object> ExecuteAction { get; set; }
        public Func<object, bool> CanExecuteAction { get; set; }

        public bool CanExecute(object parameter)
        {
            if (CanExecuteAction == null)
            {
                return true;
            }
            return CanExecuteAction(parameter);
        }

        public void Execute(object parameter)
        {
            if (ExecuteAction == null || !CanExecute(parameter))
            {
                return;
            }
            ExecuteAction.Invoke(parameter);
        }
    }
}