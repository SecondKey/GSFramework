using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// ��ʽ·�ɵĴ���
    /// </summary>
    public interface IChainRoutingNodeProxy : IRoutingNodeProxy
    {
        /// <summary>
        /// ��ʽ·�ɵ���һ������
        /// </summary>
        IChainRoutingNodeProxy LastProxy { get; set; }
        /// <summary>
        /// ��ʽ·�ɵ���һ������
        /// </summary>
        IChainRoutingNodeProxy NextProxy { get; set; }

    }
}