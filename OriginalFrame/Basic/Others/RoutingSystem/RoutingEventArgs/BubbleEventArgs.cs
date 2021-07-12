using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 冒泡路由事件参数
    /// </summary>
    [Initialization_Injectable]
    public class BubbleEventArgs : RoutingEventArgsBase
    {
        public override RoutingStrategy RoutingStrategy { get { return RoutingStrategy.Bubble; } }

        /// <summary>
        /// 路由匹配策略
        /// </summary>
        [Inject]
        public MatchingStrategy MatchingStrategy { get; }
        /// <summary>
        /// 匹配目标对象
        /// </summary>
        [Inject]
        public object MatchingParameter { get; }

        /// <summary>
        /// 冒泡路由事件参数
        /// </summary>
        /// <param name="token">事件令牌</param>
        /// <param name="OriginalSource">事件发起源</param>
        /// <param name="matchingStrategy">匹配策略</param>
        /// <param name="matchingParameter">匹配对象</param>
        /// <param name="parameters">匹配对象</param>
        public BubbleEventArgs(string token, object originalSource, MatchingStrategy matchingStrategy, object matchingParameter, params object[] parameters) : base(token, parameters)
        {
            OriginalSource = originalSource;
            MatchingStrategy = matchingStrategy;
            MatchingParameter = matchingParameter;
        }
    }
}