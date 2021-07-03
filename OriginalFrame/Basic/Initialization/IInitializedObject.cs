using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 这个接口仅用于描述对象是一个可初始化对象
    /// 实现了该接口的类在被依赖注入工厂创建后会自动初始化
    /// 具体使用请参考InitializationAttribute
    /// </summary>
    public interface IInitializableObject { }
}