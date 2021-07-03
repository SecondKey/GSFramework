using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 带有中间函数的可初始化对象
    /// 具体使用请参考InitializationAttribute
    /// </summary>
    public interface IInitializableObjectWithMiddleFunction : IInitializableObject
    {
        void MiddleInitFunction();
    }
}