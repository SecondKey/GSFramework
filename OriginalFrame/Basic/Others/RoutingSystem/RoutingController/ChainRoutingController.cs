using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace GSFramework
{
    public class ChainRoutingController : StackRoutingController, IChainRoutingController
    {
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
    }
}