using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由节点初始化特性。
    /// 如果使用该特性，需要手动为对象添加代理ID。
    /// 当ID的格式为特性实参允许类型时，直接在特性中设置Identify可以使用静态ID。
    /// 使用动态ID需要在目标对象中设置一个公共属性，并将IdentifyProperty设置为目标属性名。初始化器会根据属性名查找目标对象。
    /// 也可以使用RoutingIdentifyPropertyAttribute标记属性来代替设置IdentifyProperty。
    /// 各种ID优先级如下：
    /// 1:RoutingIdentifyPropertyAttribute标记的属性
    /// 2.IdentifyProperty标识的属性。
    /// 3.静态ID。
    /// 该初始化在中间函数后执行，在构造函数，注入函数或中间函数为IdentifyProperty属性赋值即可
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class Initialization_RoutingNodeAttribute : InitializationAttributeBase
    {
        /// <summary>
        /// 静态ID
        /// </summary>
        public object Identify { get; set; }

        /// <summary>
        /// 动态ID
        /// </summary>
        public string IdentifyProperty { get; set; }
        /// <summary>
        /// 路由参数
        /// </summary>
        public object[] parameters { get; set; }


        /// <summary>
        /// 目标路由块
        /// </summary>
        public string RoutingBlockID { get; } 

        public Initialization_RoutingNodeAttribute(string routingBlockID) : base(AppConst.Init_RoutingNode, AppConst.InitTime_After)
        {
            RoutingBlockID = routingBlockID;
        }
    }
}