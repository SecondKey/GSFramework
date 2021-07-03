using GSFramework.MVVM;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GSFramework.Default
{
    public class SimpleTextBinder : IUIBinder
    {
        Text text;
        public Text Text { get { return text; } set { text = value; } }

        public void Binding(UIBehaviour uiComponent, UIBindingComponent bindingComponent, IUILogicalNode target)
        {
            Text = uiComponent as Text;
            string path = bindingComponent.GetBindingPath("Text");
            target.BindingComponent(path, this);
            Text.text = target.DataContext.GetData(path) as string;
        }

        public void PropertyChanged(string propertyName, object NewValue)
        {
            Text.text = NewValue as string;
        }
    }
}