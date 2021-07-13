using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// ��ʽ·�ɽڵ�Ĵ���
    /// </summary>
    public class ChainRoutingNodeProxy : IChainRoutingNodeProxy
    {
        /// <summary>
        /// �¼��������б�
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