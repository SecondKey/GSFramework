using GSFramework.Default;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GSFramework.MVVM
{
    public class UIBinderProxy : ILinkedListNode<IUIBinder>, IInitializableObject
    {
        [Inject(ParametersGetMode = AppConst.Injection_Additional)]
        public IUIBinder Identify { get; set; }
        public ILinkedListNode<IUIBinder> NextNode { get; private set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }

        #region Handler
        [EventBinding]
        void PropertyChanged(EventArgs args)
        {
            PropertyChangedEventArgs tmpArgs = args as PropertyChangedEventArgs;
            Identify.PropertyChanged(tmpArgs.PropertyName, tmpArgs.Value);
        }
        #endregion

        public void AddNode(ILinkedListNode<IUIBinder> node)
        {
            if (NextNode == null)
            {
                NextNode = node;
            }
            else
            {
                NextNode.AddNode(node);
            }
        }

        public void HandleEvent(EventArgs args)
        {
            Handlers[args.Token].Invoke(args);
            if (NextNode != null)
            {
                NextNode.HandleEvent(args);
            }
        }

        public void RemoveNode(IUIBinder identify)
        {
            throw new System.NotImplementedException();
        }

        public void Initialization() { }


        #region Useless

        public void AddNode(IUIBinder binder) { throw new System.NotImplementedException(); }

        public void AddNode(IUIBinder nodeIdentify, IUIBinder token) { throw new System.NotImplementedException(); }

        public void AddNode(ILinkedListNode<IUIBinder> node, IUIBinder token) { throw new System.NotImplementedException(); }
        #endregion
    }
}