using GSFramework.Default;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework.MVVM
{
    [Injectable_Initialization]
    [Observable_Initialization]
    [UIView_Initialization]
    public abstract class UILogicNodeBase : MonoBehaviour, IUILogicalNode, IInitializableObjectWithMiddleFunction
    {
        public virtual IObservableObject Model { get { return DataContext.Model; } set { DataContext.Model = value; } }
        public virtual IUIViewModel DataContext { get { return /*(Parent as IUILogicalNode).DataContext;*/null; } set { } }
        public Dictionary<string, EventHandler> RoutedHandlers { get; set; }

        public string NodeToken { get; set; }

        public GameObject VisualParent { get { return transform.parent.gameObject; } set { transform.SetParent(value.transform, true); } }

        public int Deep { get; }
        public string Identify { get { return gameObject.name; } }
        //public Dictionary<string, ITreeNode> NextCollection { get; set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }

        #region Data
        Dictionary<string, UIBinderProxy> DataBindingList = new Dictionary<string, UIBinderProxy>();
        public void BindingComponent(string objectName, IUIBinder binder)
        {
            if (GetType().GetProperty(objectName) != null)
            {
                UIBinderProxy proxy = BasicManager.Instence.GetNewObject<UIBinderProxy>("", "", new Dictionary<string, object>() { { "Identify", binder } });
                if (!DataBindingList.ContainsKey(objectName))
                {
                    DataBindingList.Add(objectName, proxy);
                }
                else
                {
                    DataBindingList[objectName].AddNode(proxy);
                }
            }
            else
            {
                DataContext.BindingComponent(objectName, binder);
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (DataBindingList.ContainsKey(propertyName))
            {
                DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, GetType().GetProperty(propertyName).GetValue(this)));
            }
        }

        public void RaisePropertyChanged<T>(Expression<System.Func<T>> propertyExpression)
        {
            string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            if (DataBindingList.ContainsKey(propertyName))
            {
                DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, propertyExpression.Compile().Invoke()));
            }
        }
        #endregion

        #region Command
        public void ExecuteCommand(string command, object parameter)
        {
            if (GetType().GetMethod(command) != null)
            {
                GetType().GetMethod(command).Invoke(this, new object[] { parameter });
            }
            else
            {
                DataContext.ExecuteCommand(command, parameter);
            }
        }
        #endregion 

        public GameObject GetNode()
        {
            throw new System.NotImplementedException();
        }

        public void HandleEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveDeep(int deep)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveNode(string identify)
        {
            throw new System.NotImplementedException();
        }

        public void HandleRoutedEvent(RoutedEventArgs args)
        {
            if (RoutedHandlers.ContainsKey(args.Token))
            {
                RoutedHandlers[args.Token].Invoke(args);
            }
        }

        public GameObject FindVisualNode(string name)
        {
            throw new System.NotImplementedException();
        }

        public GameObject FindVisualChild(string name)
        {
            throw new System.NotImplementedException();
        }

        //public IRoutedNode<string> FindChild(string identify)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IRoutedNode<string> FindParent(string identify)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public IRoutedNode<string> FindParent(int relativePosition)
        //{
        //    if (transform.parent == null)
        //        return null;
        //    Transform tmp = transform.parent;
        //    for (int i = 0; i < relativePosition; i++)
        //    {
        //        while (tmp.GetComponent<IUILogicalNode>() == null && tmp.parent != null)
        //        {
        //            tmp = tmp.parent;
        //        }
        //    }
        //    Debug.Log(tmp.GetComponent<IUILogicalNode>());
        //    return tmp != null ? tmp.GetComponent<IUILogicalNode>() : null;
        //}


        #region Useless
        public ILinkedListNode<string> NextNode => throw new System.NotImplementedException();

        public void AddNode(string nodeIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(string nodeIdentify, string token)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<string> node)
        {
            throw new System.NotImplementedException();
        }

        public void AddNode(ILinkedListNode<string> node, string token)
        {
            throw new System.NotImplementedException();
        }

        public void MiddleInitFunction()
        {
            BasicManager.Instence.GetSingleton<UIManager>().RegistUITree(this as UIRootBase);
        }
        #endregion
    }
}