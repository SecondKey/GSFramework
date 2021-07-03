using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

namespace GSFramework.MVVM
{
    public class UIBindingInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            MonoBehaviour targetObject = (MonoBehaviour)initializedObject;
            foreach (GameObject g in targetObject.gameObject.GetInsideUI())
            {
                IUINode uiNode = g.GetComponent<IUINode>();
                if (uiNode != null)
                {
                    UIBindingComponent[] components = g.GetComponents<UIBindingComponent>();
                    foreach (UIBehaviour ui in g.GetComponents<UIBehaviour>())
                    {
                        UIBindingComponent[] targetBindingComponent = components.Where(p => p.targetComponent == ui.GetType().Name).ToArray();
                        if (targetBindingComponent.Length > 0)
                        {
                           BasicManager.Instence.GetNewObject<IUIBinder>( ui.GetType().Name).Binding(ui, targetBindingComponent.First(), targetObject as IUILogicalNode);
                        }
                    }
                }
            }
        }
    }
}