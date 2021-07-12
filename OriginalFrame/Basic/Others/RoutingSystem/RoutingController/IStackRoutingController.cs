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
        /// 添加一个节点,该节点会由RoutingController动态创建
        /// </summary>
        /// <param name="identify">节点的ID</param>
        void AddNode(object identify);
        /// <summary>
        /// 添加一个已有的节点
        /// </summary>
        /// <param name="node">目标节点</param>
        /// <param name="identify">节点的ID</param>
        void AddNode(object node, object identify);

        /// <summary>
        /// 从末位移除一个节点
        /// </summary>
        void RemoveNode();

        /// <summary>
        /// 移除一个节点
        /// </summary>
        /// <param name="target">移除的目标</param>
        /// <param name="subsequent">是否移除和后续的节点</param>
        /// <param name="strategy">移除目标的匹配策略</param>
        void RemoveNode(object target, MatchingStrategy strategy, bool removeSubsequent);
    }
}