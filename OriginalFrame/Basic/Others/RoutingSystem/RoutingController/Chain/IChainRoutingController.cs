using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IChainRoutingController : IRoutingController
    {

        /// <summary>
        /// 插入一个节点,该节点会由RoutingController动态创建
        /// </summary>
        /// <param name="identify">节点的ID</param>
        /// <param name="previous">插入位置的前一个节点。Null：插入在首位。没有合适的匹配：插入在末位</param>
        /// <param name="strategy">前一个节点的匹配策略</param>
        /// <param name="parameters">插入参数</param>
        void InsertNode(object identify, object previous, MatchingStrategy strategy);

        /// <summary>
        /// 插入一个已有的节点
        /// </summary>
        /// <param name="identify">节点的ID</param>
        /// <param name="previous">插入位置的前一个节点。Null：插入在首位。没有合适的匹配：插入在末位</param>
        /// <param name="strategy">前一个节点的匹配策略</param>
        /// <param name="parameters">插入参数</param>
        void InsertNode(object node, object identify, object previous, MatchingStrategy strategy);

        /// <summary>
        /// 将指定ID的节点替换为新的节点
        /// </summary>
        /// <param name="identify">节点的ID</param>
        /// <param name="parameters">替换参数</param>
        void ReplaceNode(object identify);
        /// <summary>
        /// 将指定ID的节点替换为目标节点
        /// </summary>
        /// <param name="node">目标节点</param>
        /// <param name="identify">节点的ID</param>
        /// <param name="parameters">替换参数</param>
        void ReplaceNode(object node, object identify);
    }
}