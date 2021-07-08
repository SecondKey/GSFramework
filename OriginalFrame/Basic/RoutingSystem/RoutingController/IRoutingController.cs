using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 基础路由控制器
    /// 负责管理一个路由结构，控制路由事件的发送和执行
    /// </summary>
    public interface IRoutingController
    {
        /// <summary>
        /// 执行事件
        /// </summary>
        void PerformEvent();
        /// <summary>
        /// 获取数据
        /// </summary>
        void GetData();
    }
}
