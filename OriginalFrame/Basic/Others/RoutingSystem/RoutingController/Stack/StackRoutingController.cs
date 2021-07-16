using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class StackRoutingController : IStackRoutingController
    {
        IChainRoutingNodeProxy firstProxy;

        #region 使用简易方法生成代理或动态创建对象，在子类中重写
        protected virtual IChainRoutingNodeProxy CreateProxy(object node, object identify)
        {
            return FrameManager.CreateInstence<IChainRoutingNodeProxy>("", "", new Dictionary<string, object>() { { "node", node }, { "identify", identify } });
        }
        protected virtual object CreateInstence()
        {
            return null;
        }
        #endregion 

        public void PerformEvent(IRoutingEventArgs args)
        {
            IChainRoutingNodeProxy proxy = firstProxy;
            while (proxy != null)
            {
                proxy.HandleEvent(args);
                switch (args.RoutingState)
                {
                    case RoutingState.Start:
                        args.RoutingState = RoutingState.Normal;
                        break;
                    case RoutingState.Skip:
                        args.RoutingState = RoutingState.Continue;
                        continue;
                    case RoutingState.Handled:
                        break;
                    default:
                        break;
                }
            }
        }

        public object GetData(IRoutingEventArgs args)
        {
            PerformEvent(args);
            return args.Results;
        }

        public void AddNode(object identify)
        {
            AddNode(CreateInstence(), identify);
        }

        public void AddNode(object node, object identify)
        {
            IChainRoutingNodeProxy proxy = CreateProxy(node, identify);
            IChainRoutingNodeProxy lastProxy = GetLastProxy();
            lastProxy.NextProxy = proxy;
            proxy.LastProxy = lastProxy;
        }



        public void RemoveNode()
        {
            IChainRoutingNodeProxy removeNode = GetLastProxy();
            removeNode.LastProxy.NextProxy = null;
        }

        public void RemoveNode(object target, MatchingStrategy strategy, bool removeSubsequent)
        {
            IChainRoutingNodeProxy removeProxy = MatchinProxy(target, strategy);
            IChainRoutingNodeProxy nextProxy = removeSubsequent ? null : removeProxy.NextProxy;
            removeProxy.LastProxy.NextProxy = nextProxy;
        }




        protected IChainRoutingNodeProxy GetLastProxy()
        {
            return MatchinProxy(-1, MatchingStrategy.Deep);
        }

        protected IChainRoutingNodeProxy MatchinProxy(object target, MatchingStrategy strategy)
        {
            if (firstProxy == null)
                return null;
            IChainRoutingNodeProxy nowProxy = firstProxy;
            switch (strategy)
            {
                case MatchingStrategy.Deep:
                    int nowDeep = 0;
                    int targetDeep = (int)target;
                    while (firstProxy.NextProxy != null)
                    {
                        if (nowDeep == targetDeep)
                            return nowProxy;
                        nowProxy = nowProxy.NextProxy;
                        nowDeep += 1;
                    }
                    return nowProxy;
                case MatchingStrategy.Identify:
                    while (firstProxy.NextProxy != null)
                    {
                        if (nowProxy.ProxyIdentify == target)
                            return nowProxy;
                        nowProxy = nowProxy.NextProxy;
                    }
                    return nowProxy;
                case MatchingStrategy.Object:
                    while (firstProxy.NextProxy != null)
                    {
                        if (nowProxy.ProxyNode == target)
                            return nowProxy;
                        nowProxy = nowProxy.NextProxy;
                    }
                    return nowProxy;
                default:
                    return null;
            }
        }
    }
}