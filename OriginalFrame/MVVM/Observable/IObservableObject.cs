using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework.MVVM
{
    public interface IObservableObject : IInitializableObject
    {
        Dictionary<string, DelegateCommand> Commands { get; set; }
        void ExecuteCommand(string command, object parameter);

        object GetData(string propertyName);
        void RaisePropertyChanged([CallerMemberName] string PropertyName = "");
        void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression);
    }
}