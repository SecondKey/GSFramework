using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 初始化结束函数特性
    /// 目标函数必须是无参的
    /// 标注了该特性的函数将会在对象初始化过程的结束过程执行
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InitEndFunctionAttribute : Attribute { }
}