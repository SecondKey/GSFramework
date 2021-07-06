using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class EventArgs
    {
        /// <summary>
        /// 执行事件的标识
        /// </summary>
        public string Token { get; }

        /// <summary>
        /// 路由策略
        /// </summary>
        public virtual RoutingStrategy Strategy { get { return RoutingStrategy.TopToBottom; } }

        /// <summary>
        /// 路由事件的当前状态
        /// </summary>
        public RoutingState State { get; set; }

        /// <summary>
        /// 事件的源，发起该事件的目标
        /// </summary>
        public object OriginalSource { get; protected set; }

        /// <summary>
        /// 随事件传递的参数
        /// </summary>
        public object[] Parameters { get; }

        /// <summary>
        /// 步值表，用于记录每一次数据处理的结果
        /// </summary>
        public List<object> StepValues { get; private set; }

        /// <summary>
        /// 初始化EventArgs实例
        /// </summary>
        /// <param name="token">事件的标识</param>
        /// <param name="source">事件的发送源</param>
        /// <param name="parameters">事件的参数</param>
        public EventArgs(string token, params object[] parametesr)
        {
            Token = token;
            Parameters = parametesr;
        }
    }
}