using GSFramework.Default;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework.MVVM
{
    [Initialization_Injectable]
    [Initialization_Observable]
    [Initialization_UIView]
    public abstract class UILogicNodeBase : MonoBehaviour, IUILogicalNode
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
        //Dictionary<string, UIBinderProxy> DataBindingList = new Dictionary<string, UIBinderProxy>();
        public void BindingComponent(string objectName, IUIBinder binder)
        {
            if (GetType().GetProperty(objectName) != null)
            {
                //UIBinderProxy proxy = BasicManager.Instence.GetNewObject<UIBinderProxy>("", "", new Dictionary<string, object>() { { "Identify", binder } });
                //if (!DataBindingList.ContainsKey(objectName))
                //{
                //    DataBindingList.Add(objectName, proxy);
                //}
                //else
                //{
                //    DataBindingList[objectName].AddNode(proxy);
                //}
            }
            else
            {
                DataContext.BindingComponent(objectName, binder);
            }
        }

        public void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            //if (DataBindingList.ContainsKey(propertyName))
            //{
            //    DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, GetType().GetProperty(propertyName).GetValue(this)));
            //}
        }

        public void RaisePropertyChanged<T>(Expression<System.Func<T>> propertyExpression)
        {
            //string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            //if (DataBindingList.ContainsKey(propertyName))
            //{
            //    DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, propertyExpression.Compile().Invoke()));
            //}
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

        public void HandleEvent(IRoutingEventArgs args)
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

        public void HandleRoutedEvent(BubbleEventArgs args)
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

        [InitMiddleFunction]
        public void Init()
        {
            FrameManager.GetInstence<UIManager>().RegistUITree(this as UIRootBase);
        }
    }
}