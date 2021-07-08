using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IObjectPool
    {
        /// <summary>
        /// 获取正在使用的对象
        /// </summary>
        /// <returns></returns>
        object GetActionObject();
        /// <summary>
        /// 获取闲置的对象
        /// </summary>
        /// <returns></returns>
        object GetIdleObject();
    }
}