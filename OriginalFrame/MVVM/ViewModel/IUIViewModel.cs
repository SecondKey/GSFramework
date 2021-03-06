using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public interface IUIViewModel : IObservableObject, IObserver
    {
        IObservableObject Model { get; set; }

        void BindingComponent(string objectName, IUIBinder binder);
    }
}