using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.Events;
using GSFramework.MVVM;

namespace GSFramework.Default
{
    public class SimpleButtonBinder : IUIBinder
    {
        Button button;
        public void Binding(UIBehaviour uiComponent, UIBindingComponent bindingComponent, IUILogicalNode target)
        {
            button = uiComponent as Button;
            button.onClick.AddListener(() => { target.ExecuteCommand(bindingComponent.GetBindingPath("OnClick"), null); });
        }

        public void PropertyChanged(string propertyName, object NewValue)
        {
            throw new System.NotImplementedException();
        }
    }
}