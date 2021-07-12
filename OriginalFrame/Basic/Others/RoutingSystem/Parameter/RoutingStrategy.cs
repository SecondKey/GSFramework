using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由策略,自顶向下为正方向
    /// </summary>
    public enum RoutingStrategy
    {
        /// <summary>
        /// 终止路由
        /// </summary>
        None,
        /// <summary>
        /// 指定目标的路由
        /// </summary>
        Target,
        /// <summary>
        /// 自当前节点向目标节点的路由
        /// </summary>
        ToTarget,
        /// <summary>
        /// 自目标节点向当前节点的路由
        /// </summary>
        FromTarget,
        /// <summary>
        /// 冒泡路由，自当前节点向顶节点的路由
        /// </summary>
        Bubble,
        /// <summary>
        /// 反向冒泡路由，自底节点向当前节点的路由
        /// </summary>
        ReverseBubble,
        /// <summary>
        /// 隧道路由，自顶节点向当前节点的路由
        /// </summary>
        Tunnel,
        /// <summary>
        /// 反向隧道路由，自当前节点向底节点的路由
        /// </summary>
        ReverseTunnel,
        /// <summary>
        /// 自顶向底全部节点的路由
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 自底向顶全部节点的路由
        /// </summary>
        BottonToTop,
    }
}