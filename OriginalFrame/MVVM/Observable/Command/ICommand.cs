using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public interface ICommand
    {
        bool CanExecute(object parameter);
        void Execute(object parameter);
    }
}