using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IEventNodeProxy
    {
        object ProxyObject { get; }
        object ProxyIdentify { get; }

        IEventNodeProxy NextProxt { get; set; }

        Dictionary<string, EventHandler> EventHandlers { get; set; }
        Dictionary<string, EventGetter> DataProviders { get; set; }

        void PerformEvent(EventArgs args);
        object GetData(EventArgs args);

        void AddNode(object nodeIdentify);
        void AddNode(IEventNodeProxy node);
        void AddNode(object nodeIdentify, object token);
        void AddNode(IEventNodeProxy node, object token);

        void RemoveNodeByObject(object proxyObject);
        void RemoveNodeByIdentify(object proxyIdentify);
    }
}