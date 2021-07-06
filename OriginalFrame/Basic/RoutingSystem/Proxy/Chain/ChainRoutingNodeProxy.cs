using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ChainRoutingNodeProxy : IChainRoutingNodeProxy
    {
        /// <summary>
        /// 事件处理器列表
        /// </summary>
        Dictionary<string, EventHandler> EventHandlers { get; set; }
        /// <summary>
        /// 数据处理器列表
        /// </summary>
        Dictionary<string, DataProvider> DataProviders { get; set; }



        public IChainRoutingNodeProxy LastProxy { get; }

        public IChainRoutingNodeProxy NextProxy { get; }

        public void Add(object proxyIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void Add(IChainRoutingNodeProxy proxy)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(object proxyIdentify, object lastIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void Insert(IChainRoutingNodeProxy proxy, object lastIdentify)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveByNode(object node, bool Subsequent = false)
        {
            throw new System.NotImplementedException();
        }

        public void RemoveByIdentify(object proxyIdentify, bool Subsequent = false)
        {
            throw new System.NotImplementedException();
        }

        public object ProxyNode => throw new System.NotImplementedException();

        public object ProxyIdentify => throw new System.NotImplementedException();



        public void RegistEventHandler(string eventToken, EventHandler handler)
        {
            throw new System.NotImplementedException();
        }

        public void RegistDataProviders(string eventToken, DataProvider provider)
        {
            throw new System.NotImplementedException();
        }

        public void PerformEvent(EventArgs args)
        {
            throw new System.NotImplementedException();
        }

        public object GetData(EventArgs args)
        {
            throw new System.NotImplementedException();
        }
    }
}