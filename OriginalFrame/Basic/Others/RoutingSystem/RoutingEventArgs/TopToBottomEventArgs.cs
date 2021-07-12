using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class TopToBottomEventArgs : RoutingEventArgsBase
    {
        public override RoutingStrategy RoutingStrategy { get { return RoutingStrategy.TopToBottom; } }

        public TopToBottomEventArgs(string token, params object[] parametesr) : base(token, parametesr)
        {
        }
    }
}