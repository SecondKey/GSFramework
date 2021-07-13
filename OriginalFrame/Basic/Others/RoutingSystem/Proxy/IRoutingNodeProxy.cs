using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由代理节点的基础接口
    /// </summary>
    public interface IRoutingNodeProxy
    {
        /// <summary>
        /// 目标代理节点
        /// </summary>
        object ProxyNode { get; }
        /// <summary>
        /// 代理的标识
        /// </summary>
        object ProxyIdentify { get; }

        /// <summary>
        /// 注册事件处理器
        /// </summary>
        /// <param name="eventToken">事件令牌</param>
        /// <param name="handler">事件处理器</param>
        void RegistEventHandler(string eventToken, EventHandler handler);

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="args">事件参数</param>
        void HandleEvent(IRoutingEventArgs args);
    }
}