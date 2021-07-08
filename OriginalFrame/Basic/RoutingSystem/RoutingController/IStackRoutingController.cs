using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 栈式路由的控制器
    /// 允许在末位动态添加或移除节点
    /// </summary>
    public interface IStackRoutingController : IRoutingController
    {
        /// <summary>
        /// 在末位节点后添加节点
        /// </summary>
        /// <param name="node">目标节点</param>
        /// <param name="identify">节点的ID</param>
        void AddNode(object node, object identify);
        /// <summary>
        /// 移除末位一个节点
        /// </summary>
        void RemoveNode();
    }
}