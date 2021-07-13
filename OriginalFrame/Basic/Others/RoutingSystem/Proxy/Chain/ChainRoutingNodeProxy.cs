using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 链式路由节点的代理
    /// </summary>
    public class ChainRoutingNodeProxy : IChainRoutingNodeProxy
    {
        /// <summary>
        /// 事件处理器列表
        /// </summary>
        Dictionary<string, EventHandler> EventHandlers { get; set; }

        public IChainRoutingNodeProxy LastProxy { get; set; }
        public IChainRoutingNodeProxy NextProxy { get; set; }

        public object ProxyNode { get; }
        public object ProxyIdentify { get; }


        public void RegistEventHandler(string eventToken, EventHandler handler)
        {
            EventHandlers.Add(eventToken, handler);
        }

        public void HandleEvent(IRoutingEventArgs args)
        {
            EventHandlers[args.Token].Invoke(args);
        }
    }
}