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
        /// 注册的事件处理器
        /// </summary>
        Dictionary<string, EventHandler> EventHandlers { get; set; }
        /// <summary>
        /// 注册的数据处理器
        /// </summary>
        Dictionary<string, DataProvider> DataProviders { get; set; }

        /// <summary>
        /// 执行事件
        /// </summary>
        /// <param name="args">事件参数</param>
        void PerformEvent(EventArgs args);
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="args">事件参数</param>
        /// <returns>目标值或处理后的值</returns>
        object GetData(EventArgs args);
    }
}