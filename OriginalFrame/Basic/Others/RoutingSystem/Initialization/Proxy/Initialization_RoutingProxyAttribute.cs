using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由代理初始化特性
    /// 这个特性仅针对路由代理类
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Initialization_RoutingProxyAttribute : InitializationAttributeBase
    {
        public Initialization_RoutingProxyAttribute() : base(AppConst.Init_RoutingProxy, AppConst.InitTime_Before) { }
    }
}