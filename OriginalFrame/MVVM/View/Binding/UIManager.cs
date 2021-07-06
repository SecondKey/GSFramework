using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class UIManager
    {
        Dictionary<string, UIRootBase> uiTrees = new Dictionary<string, UIRootBase>();
        public void RegistUITree(UIRootBase uiTree)
        {
            uiTrees.Add(uiTree.Identify, uiTree);
        }
        public void UnRegistUITree(UIRootBase uiTree)
        {
            UnRegistUITree(uiTree.Identify);
        }
        public void UnRegistUITree(string uiTreeName)
        {
            uiTrees.Remove(uiTreeName);
        }

        public List<KeyValuePair<IUILogicalNode, BubbleEventArgs>> RoutedLis = new List<KeyValuePair<IUILogicalNode, BubbleEventArgs>>();
        public void LaunchRoutedEvent(IUILogicalNode Launcher, BubbleEventArgs args)
        {
            foreach (IUILogicalNode node in GetUINode(Launcher, args.Strategy))
            {
                if (args.Handled)
                {
                    break;
                }
                //node.HandleRoutedEvent(args);
            }
            RoutedEventOver();
        }

        public void InsertRoutedEvent()
        {

        }

        public void RedirectionRoutedEvent(IUILogicalNode redirector, BubbleEventArgs args)
        {

        }

        public void HandlerRoutedEvent()
        {

        }

        public void RoutedEventOver()
        {

        }

        //TODO:GetUINode未全部实现
        IEnumerable GetUINode(IUINode node, RoutingStrategy strategy)
        {
            switch (strategy)
            {
                case RoutingStrategy.Bubble:
                    yield return node;
                    //if (node.Parent != null)
                    //{
                    //    foreach (IRoutedNode parentRoot in GetUINode(node.Parent as IRoutedNode, RoutedStrategy.Bubble))
                    //    {
                    //        yield return parentRoot;
                    //    }
                    //}
                    break;
                case RoutingStrategy.Tunnel:
                    break;
                case RoutingStrategy.ReverseBubble:
                    break;
                case RoutingStrategy.ReverseTunnel:
                    break;
                case RoutingStrategy.BottonToTop:
                    break;
                case RoutingStrategy.TopToBottom:
                    break;
            }
        }
    }
}