using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 链式路由的代理
    /// </summary>
    public interface IChainRoutingNodeProxy : IRoutingNodeProxy
    {
        /// <summary>
        /// 链式路由的上一个代理
        /// </summary>
        IChainRoutingNodeProxy LastProxy { get; }
        /// <summary>
        /// 链式路由的下一个代理
        /// </summary>
        IChainRoutingNodeProxy NextProxy { get; }

        /// <summary>
        /// 在路由链的末尾添加一个代理。
        /// 如果路由链代理的目标节点是统一的类型，则可以由代理创建。
        /// </summary>
        /// <param name="proxyIdentify">要添加的代理的ID</param>
        void Add(object proxyIdentify);
        /// <summary>
        /// 在路由链的末尾添加一个代理
        /// </summary>
        /// <param name="proxy">要添加的代理</param>
        void Add(IChainRoutingNodeProxy proxy);

        /// <summary>
        /// 在路由链指定位置插入一个代理。
        /// 如果路由链代理的目标节点是统一的类型，则可以由代理创建。
        /// </summary>
        /// <param name="proxyIdentify">要插入的代理的ID</param>
        /// <param name="lastIdentify">插入位位置前一个节点的Token，若为Null则为首位，若无法匹配则为末尾</param>
        void Insert(object proxyIdentify, object lastIdentify);
        /// <summary>
        /// 在路由链指定位置插入一个代理。
        /// </summary>
        /// <param name="proxy">要插入的代理</param>
        /// <param name="lastIdentify">插入位位置前一个节点的Token，若为Null则为首位，若无法匹配则为末尾</param>
        void Insert(IChainRoutingNodeProxy proxy, object lastIdentify);

        /// <summary>
        /// 根据目标节点移除代理
        /// </summary>
        /// <param name="node">目标的节点</param>
        /// <param name="Subsequent">是否移除后续的节点，移除为true，保留为false</param>
        void RemoveByNode(object node, bool Subsequent = false);

        /// <summary>
        /// 根据目标节点移除代理
        /// </summary>
        /// <param name="proxyIdentify">目标的标识</param>
        /// <param name="Subsequent">是否移除后续的节点，移除为true，保留为false</param>
        void RemoveByIdentify(object proxyIdentify, bool Subsequent = false);
    }
}