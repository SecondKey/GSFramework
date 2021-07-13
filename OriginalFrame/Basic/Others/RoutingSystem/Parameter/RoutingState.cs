using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public enum RoutingState
    {
        /// <summary>
        /// 事件首次被处理
        /// </summary>
        Start,
        /// <summary>
        /// 路由事件正常执行
        /// </summary>
        Normal,
        /// <summary>
        /// 路由事件跳过下一个节点
        /// </summary>
        Skip,
        /// <summary>
        /// 路由事件跳过了上一个节点
        /// </summary>
        Continue,

        /// <summary>
        /// 路由事件已被处理完毕
        /// </summary>
        Handled,
        /// <summary>
        /// 路由事件异常中断
        /// </summary>
        Interrupt,
    }
}