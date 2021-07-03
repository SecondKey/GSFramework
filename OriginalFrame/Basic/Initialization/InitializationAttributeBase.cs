using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 该特性是对象初始化特性的基类
    /// 该特性的所有派生在仅在 作用于实现了IInitializableObject接口的类 时有效
    /// 该类的派生类用于描述 目标类创建的对象初始化时使用的初始化器，一个对象可以被多个不同的初始化器初始化
    /// 一般情况下在对象被创建时自动进行初始化，特殊情况下可以手动调用初始化（例如场景中挂在的MonoBehaviour脚本）
    /// 调用初始化器对目标对象的处理有两个阶段，以初始化中间函数为间隔分为 中间函数前 和 中间函数后。中间函数在IInitializableObjectWithMiddleFunction中
    /// 在每个阶段的内部顺序由获取特性的顺序决定。
    /// 大部分初始化操作在中间函数前执行（包括注入，对属性的赋值等）
    /// 当初始化操作需要中间函数的执行，或初始化链需要逆序遍历时，在中间函数后执行（包括消息注册需要中间函数的消息参数，自底向上构建UI逻辑树等）
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
    public abstract class InitializationAttributeBase : Attribute
    {
        /// <summary>
        /// 初始化器类型，在配置表中配置
        /// </summary>
        public string InitializerType { get; }
        /// <summary>
        /// 初始化的阶段（中间函数前或中间函数后）
        /// </summary>
        public string InitializeTime { get; }
        /// <summary>
        /// 目标初始化器的ID，在配置表中配置
        /// </summary>
        public string InitializerID { get; set; }

        public InitializationAttributeBase(string initializeType, string initializeTime)
        {
            InitializerType = initializeType;
            InitializeTime = initializeTime;
        }
    }
}