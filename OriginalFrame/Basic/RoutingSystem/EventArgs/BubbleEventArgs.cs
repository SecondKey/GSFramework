using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class BubbleEventArgs : EventArgs
    {

        /// <summary>
        /// 路由事件的路由标识
        /// </summary>
        public override RoutingStrategy Strategy { get { return RoutingStrategy.Bubble; } }

        /// <summary>
        /// 指定执行人
        /// </summary>
        public virtual object Performer { get; }



        /// <summary>
        /// 事件的发送源
        /// </summary>
        public object Source { get; set; }


        public BubbleEventArgs(RoutingStrategy strategy, string token) : base(token)
        {
        }

        public BubbleEventArgs(RoutingStrategy strategy, string token, string source) : base(token, source)
        {
            Source = source;
            OriginalSource = source;
        }

        public BubbleEventArgs(RoutingStrategy strategy, string token, string source, string originalSource) : base(token, source)
        {
            Strategy = strategy;
            Source = source;
            OriginalSource = originalSource;
        }
    }
}