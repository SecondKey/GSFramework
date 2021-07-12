using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public abstract class StackRoutingController : IStackRoutingController
    {
        IChainRoutingNodeProxy firstProxy;

        #region 使用简易方法生成代理或动态创建对象，在子类中重写
        protected virtual IChainRoutingNodeProxy CreateProxy(object node, object identify)
        {
            return BasicCenter.CreateInstence<IChainRoutingNodeProxy>("", "", new Dictionary<string, object>() { { "node", node }, { "identify", identify } });
        }
        protected virtual object CreateInstence()
        {
            return null;
        }
        #endregion 

        public void PerformEvent(IRoutingEventArgs args)
        {
            throw new NotImplementedException();
        }

        public object GetData(IRoutingEventArgs args)
        {
            throw new NotImplementedException();
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

        public void InsertNode(object identify, object previous, MatchingStrategy strategy)
        {
            InsertNode(CreateInstence(), identify, previous, strategy);
        }

        public void InsertNode(object node, object identify, object previous, MatchingStrategy strategy)
        {
            IChainRoutingNodeProxy proxy = CreateProxy(node, identify);
            IChainRoutingNodeProxy lastProxy = MatchinProxy(previous, strategy);
            IChainRoutingNodeProxy nextProxy = lastProxy.NextProxy;
            lastProxy.NextProxy = proxy;
            proxy.LastProxy = lastProxy;
            proxy.NextProxy = nextProxy;
            nextProxy.LastProxy = proxy;
        }

        public void ReplaceNode(object identify)
        {
            ReplaceNode(CreateInstence(), identify);
        }

        public void ReplaceNode(object node, object identify)
        {
            IChainRoutingNodeProxy proxy = CreateProxy(node, identify);

            IChainRoutingNodeProxy replaceProxy = MatchinProxy(identify, MatchingStrategy.Identify);
            IChainRoutingNodeProxy lastProxy = replaceProxy.LastProxy;
            IChainRoutingNodeProxy nextProxy = replaceProxy.NextProxy;

            lastProxy.NextProxy = proxy;
            proxy.LastProxy = lastProxy;
            proxy.NextProxy = nextProxy;
            nextProxy.LastProxy = proxy;

        }

        public void RemoveNode()
        {

        }

        public void RemoveNode(object target, MatchingStrategy strategy, bool removeSubsequent)
        {

        }




        IChainRoutingNodeProxy GetLastProxy()
        {
            return MatchinProxy(-1, MatchingStrategy.Deep);
        }

        IChainRoutingNodeProxy MatchinProxy(object target, MatchingStrategy strategy)
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