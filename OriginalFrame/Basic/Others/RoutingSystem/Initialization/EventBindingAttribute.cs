using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由事件绑定特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EventBindingAttribute : Attribute
    {
        /// <summary>
        /// 事件的令牌
        /// </summary>
        public string EventToken { get; }
        /// <summary>
        /// 使用函数名作为事件令牌
        /// </summary>
        public EventBindingAttribute() { }
        /// <summary>
        /// 使用给定的事件令牌
        /// </summary>
        /// <param name="eventToken">事件令牌</param>
        public EventBindingAttribute(string eventToken)
        {
            EventToken = eventToken;
        }
    }
}