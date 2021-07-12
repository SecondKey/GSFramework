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

        public IChainRoutingNodeProxy LastProxy { get; set; }
        public IChainRoutingNodeProxy NextProxy { get; set; }

        public object ProxyNode { get; }
        public object ProxyIdentify { get; }


        public void RegistEventHandler(string eventToken, EventHandler handler)
        {
            EventHandlers.Add(eventToken, handler);
        }

        public void RegistDataProviders(string eventToken, DataProvider provider)
        {
            DataProviders.Add(eventToken, provider);
        }

        public void PerformEvent(IRoutingEventArgs args)
        {
            EventHandlers[args.Token].Invoke(args);
        }

        public object GetData(IRoutingEventArgs args)
        {
            return DataProviders[args.Token].Invoke(args);
        }

    }
}