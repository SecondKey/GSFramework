using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;
using GSFramework;
using static GSFramework.AppConst;

namespace GSFramework.MVVM
{
    [Observable_Initialization]
    public class ObservableObject : IObservableObject
    {
        public Dictionary<string, DelegateCommand> Commands { get; set; } = new Dictionary<string, DelegateCommand>();

        public virtual void ExecuteCommand(string command, object parameter)
        {
            if (Commands.ContainsKey(command))
            {
                Commands[command].Execute(parameter);
            }
        }

        public virtual object GetData(string propertyName)
        {
            PropertyInfo property = GetType().GetProperty(propertyName);
            if (property != null)
            {
                return property.GetValue(this);
            }
            else
            {
                return null;
            }
        }

        public virtual void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            BasicManager.Instence.GetSingleton<IObservableManager>().PropertyChange(this, propertyName, GetType().GetProperty(propertyName).GetValue(this));
        }

        public virtual void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            BasicManager.Instence.GetSingleton<IObservableManager>().PropertyChange(this, propertyName, propertyExpression.Compile().Invoke());
        }

        public virtual void Initialization() { }
    }
}