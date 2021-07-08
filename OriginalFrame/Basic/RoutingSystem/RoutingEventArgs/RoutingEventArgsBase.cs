using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public abstract class RoutingEventArgsBase : IRoutingEventArgs
    {
        public abstract RoutingStrategy RoutingStrategy { get; }
        public RoutingState RoutingState { get; set; }
        public object Source { get; }
        public object OriginalSource { get; protected set; }

        public string Token { get; }
        public object[] Parameters { get; }
        public List<object> StepValues { get; private set; }

        /// <summary>
        /// 初始化EventArgs实例
        /// </summary>
        /// <param name="token">事件的标识</param>
        /// <param name="source">事件的发送源</param>
        /// <param name="parameters">事件的参数</param>
        public RoutingEventArgsBase(string token, params object[] parametesr)
        {
            Token = token;
            Parameters = parametesr;
        }

    }
}