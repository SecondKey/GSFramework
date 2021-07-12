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
        IChainRoutingNodeProxy LastProxy { get; set; }
        /// <summary>
        /// 链式路由的下一个代理
        /// </summary>
        IChainRoutingNodeProxy NextProxy { get; set; }

    }
}