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
        /// 点对点的路由
        /// </summary>
        P2P,
        /// <summary>
        /// 有目标的，自当前节点向目标节点
        /// </summary>
        Targeted,
        /// <summary>
        /// 反向目标，自目标节点向当前节点
        /// </summary>
        ReverseTargeted,
        /// <summary>
        /// 冒泡路由，自当前节点向顶节点
        /// </summary>
        Bubble,
        /// <summary>
        /// 反向冒泡路由，自底节点向当前节点
        /// </summary>
        ReverseBubble,
        /// <summary>
        /// 隧道路由，自顶节点向当前节点
        /// </summary>
        Tunnel,
        /// <summary>
        /// 反向隧道路由，自当前节点向底节点
        /// </summary>
        ReverseTunnel,
        /// <summary>
        /// 自顶向底全部节点
        /// </summary>
        TopToBottom,
        /// <summary>
        /// 自底向顶全部节点
        /// </summary>
        BottonToTop,
    }
}