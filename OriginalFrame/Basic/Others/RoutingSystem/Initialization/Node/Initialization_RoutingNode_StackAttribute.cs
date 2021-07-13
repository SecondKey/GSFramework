using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 栈式路由节点的初始化特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Initialization_RoutingNode_StackAttribute : Initialization_RoutingNodeAttribute
    {
        public Initialization_RoutingNode_StackAttribute(string routingBlockID) : base(routingBlockID, AppConst.Init_RoutingNode_Stack) { }
    }
}