using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由代理初始化特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RoutingProxyInitializationAttribute : InitializationAttributeBase
    {
        public RoutingProxyInitializationAttribute() : base(AppConst.Init_RoutingProxy, AppConst.InitTime_Before) { }
    }
}