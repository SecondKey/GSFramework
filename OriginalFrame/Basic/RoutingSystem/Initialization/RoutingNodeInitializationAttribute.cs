﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由节点初始化特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class RoutingNodeInitializationAttribute : InitializationAttributeBase
    {
        public RoutingNodeInitializationAttribute() : base(AppConst.Init_RoutingNode, AppConst.InitTime_Before) { }
    }
}