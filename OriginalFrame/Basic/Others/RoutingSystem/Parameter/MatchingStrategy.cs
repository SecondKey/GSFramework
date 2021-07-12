using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GSFramework
{
    /// <summary>
    /// 路由对象匹配策略
    /// </summary>
    public enum MatchingStrategy
    {
        /// <summary>
        /// 匹配代理对象
        /// </summary>
        Object = 0,
        /// <summary>
        /// 匹配id
        /// </summary>
        Identify = 1,
        /// <summary>
        /// 匹配路由深度
        /// </summary>
        Deep = 2,
    }
}