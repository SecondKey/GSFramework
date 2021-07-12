using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 初始化器接口
    /// 它由初始化管理器反射创建，每种类型的初始化器在程序运行过程中有且仅有一个
    /// </summary>
    public interface IInitializer
    {
        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="initializedObject">要初始化的对象</param>
        void Initialization(object initializedObject);
    }
}