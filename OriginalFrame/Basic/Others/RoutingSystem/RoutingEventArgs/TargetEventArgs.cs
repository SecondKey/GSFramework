using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class TargetEventArgs : RoutingEventArgsBase
    {
        public override RoutingStrategy RoutingStrategy { get { return RoutingStrategy.Target; } }

        public TargetEventArgs(string token, params object[] parametesr) : base(token, parametesr)
        {

        }

    }
}
