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
        IChainRoutingNodeProxy LastProxy { get; }
        /// <summary>
        /// ��ʽ·�ɵ���һ������
        /// </summary>
        IChainRoutingNodeProxy NextProxy { get; }

        /// <summary>
        /// ��·������ĩβ���һ������
        /// ���·���������Ŀ��ڵ���ͳһ�����ͣ�������ɴ�������
        /// </summary>
        /// <param name="proxyIdentify">Ҫ��ӵĴ����ID</param>
        void Add(object proxyIdentify);
        /// <summary>
        /// ��·������ĩβ���һ������
        /// </summary>
        /// <param name="proxy">Ҫ��ӵĴ���</param>
        void Add(IChainRoutingNodeProxy proxy);

        /// <summary>
        /// ��·����ָ��λ�ò���һ������
        /// ���·���������Ŀ��ڵ���ͳһ�����ͣ�������ɴ�������
        /// </summary>
        /// <param name="proxyIdentify">Ҫ����Ĵ����ID</param>
        /// <param name="lastIdentify">����λλ��ǰһ���ڵ��Token����ΪNull��Ϊ��λ�����޷�ƥ����Ϊĩβ</param>
        void Insert(object proxyIdentify, object lastIdentify);
        /// <summary>
        /// ��·����ָ��λ�ò���һ������
        /// </summary>
        /// <param name="proxy">Ҫ����Ĵ���</param>
        /// <param name="lastIdentify">����λλ��ǰһ���ڵ��Token����ΪNull��Ϊ��λ�����޷�ƥ����Ϊĩβ</param>
        void Insert(IChainRoutingNodeProxy proxy, object lastIdentify);

        /// <summary>
        /// ����Ŀ��ڵ��Ƴ�����
        /// </summary>
        /// <param name="node">Ŀ��Ľڵ�</param>
        /// <param name="Subsequent">�Ƿ��Ƴ������Ľڵ㣬�Ƴ�Ϊtrue������Ϊfalse</param>
        void RemoveByNode(object node, bool Subsequent = false);

        /// <summary>
        /// ����Ŀ��ڵ��Ƴ�����
        /// </summary>
        /// <param name="proxyIdentify">Ŀ��ı�ʶ</param>
        /// <param name="Subsequent">�Ƿ��Ƴ������Ľڵ㣬�Ƴ�Ϊtrue������Ϊfalse</param>
        void RemoveByIdentify(object proxyIdentify, bool Subsequent = false);
    }
}