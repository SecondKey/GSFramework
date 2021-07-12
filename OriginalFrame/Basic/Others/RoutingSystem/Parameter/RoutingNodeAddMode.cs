using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由节点加入路由块的方式
    /// </summary>
    public enum RoutingNodeAddMode
    {
        /// <summary>
        /// 添加在路由快末位
        /// </summary>
        Add = 0,
        /// <summary>
        /// 匹配目标位置，将节点插入在目标位置后
        /// </summary>
        Insert = 1,
        /// <summary>
        /// 匹配目标位置，使用当前节点替换目标位置的节点
        /// </summary>
        Replace = 2,
    }
}