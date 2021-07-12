using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 用于标识对象的路由ID属性。
    /// 在一个类中仅第一个有效
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class RoutingIdentifyPropertyAttribute : Attribute { }
}