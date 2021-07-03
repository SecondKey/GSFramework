using System;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework.MVVM
{
    public interface IUILogicalNode : IUINode
    {
        IObservableObject Model { get; set; }
        IUIViewModel DataContext { get; set; }

        GameObject VisualParent { get; set; }
        GameObject FindVisualNode(string name);
        GameObject FindVisualChild(string name);

        void BindingComponent(string objectName, IUIBinder binder);
        void ExecuteCommand(string command, object parameter);

        void RaisePropertyChanged([CallerMemberName] string PropertyName = "");
        void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression);
    }
}