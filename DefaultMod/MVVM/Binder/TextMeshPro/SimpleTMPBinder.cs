using GSFramework.MVVM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;

namespace GSFramework.Default
{
    public class SimpleTMPBinder : IUIBinder
    {
        TextMeshProUGUI tmpro;
        public void Binding(UIBehaviour uiComponent, UIBindingComponent bindingComponent, IUILogicalNode target)
        {
            tmpro = uiComponent as TextMeshProUGUI;
            string path = bindingComponent.GetBindingPath("TMPro");
            target.BindingComponent(path, this);
            tmpro.text = target.DataContext.GetData(path) as string;
        }

        public void PropertyChanged(string propertyName, object NewValue)
        {
            tmpro.text = NewValue as string;
        }
    }
}