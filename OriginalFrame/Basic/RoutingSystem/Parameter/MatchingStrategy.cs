using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace GSFramework
{
    /// <summary>
    /// 路由对象匹配策略
    /// </summary>
    [Flags]
    public enum MatchingStrategy
    {
        /// <summary>
        /// 匹配代理对象
        /// </summary>
        Object,
        /// <summary>
        /// 匹配id
        /// </summary>
        Identify,
        /// <summary>
        /// 匹配路由深度
        /// </summary>
        Deep,
    }
}