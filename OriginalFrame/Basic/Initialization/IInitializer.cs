using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 初始化器接口
    /// 它由初始化管理器反射创建
    /// 
    /// </summary>
    public interface IInitializer
    {
        void Initialization(IInitializableObject initializedObject);
    }
}