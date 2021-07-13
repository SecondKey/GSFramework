using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IRoutingEventArgs
    {
        #region 路由信息
        /// <summary>
        /// 路由策略
        /// </summary>
        RoutingStrategy RoutingStrategy { get; }
        /// <summary>
        /// 路由状态
        /// </summary>
        RoutingState RoutingState { get; set; }

        /// <summary>
        /// 事件处理的源，首个处理事件的节点
        /// </summary>
        object Source { get; }
        /// <summary>
        /// 发起事件的源，发起事件的节点
        /// </summary>
        object OriginalSource { get; }
        #endregion

        #region 事件信息
        /// <summary>
        /// 事件的令牌
        /// </summary>
        string Token { get; }
        /// <summary>
        /// 随事件传递的参数
        /// </summary>
        object[] Parameters { get; }
        /// <summary>
        /// 步值表，用于记录每一次数据处理的结果
        /// </summary>
        List<object> StepValues { get; }

        object Results { get; set; }
        #endregion
    }
}