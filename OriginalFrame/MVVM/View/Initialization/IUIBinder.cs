using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSFramework.MVVM
{
    public interface IUIBinder
    {
        void Binding(UIBehaviour uiComponent, UIBindingComponent bindingComponent, IUILogicalNode target);
        void PropertyChanged(string propertyName, object NewValue);
    }
}