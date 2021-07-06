using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    ///// <summary>
    ///// 链表式消息结构(消息节点的代理)
    ///// </summary>
    //public class NotifiedObjectProxy : ILinkedListNode<IMsgComponent>
    //{
    //    public IMsgComponent Identify { get; }
    //    public ILinkedListNode<IMsgComponent> NextNode { private set; get; }
    //    public Dictionary<string, EventHandler> Handlers { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    //    public NotifiedObjectProxy(IMsgComponent notifiedObject)
    //    {
    //        Identify = notifiedObject;
    //        NextNode = null;
    //    }

    //    public void DebugList()
    //    {
    //        Debug.Log(Identify.ToString());
    //        if (NextNode != null)
    //        {
    //            (NextNode as NotifiedObjectProxy).DebugList();
    //        }
    //    }

    //    public void AddNode(IMsgComponent nodeIdentify)
    //    {
    //        if (NextNode == null)
    //        {
    //            NextNode = new NotifiedObjectProxy(nodeIdentify);
    //        }
    //        else
    //        {
    //            NextNode.AddNode(nodeIdentify);
    //        }
    //    }

    //    public void HandleEvent(EventArgs args)
    //    {
    //        Identify.ReceiveMessage(args as MessageArgs);
    //    }



    //    public void AddNode(ILinkedListNode<IMsgComponent> node)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void AddNode(IMsgComponent nodeIdentify, IMsgComponent token)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void AddNode(ILinkedListNode<IMsgComponent> node, IMsgComponent token)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void RemoveNode(IMsgComponent identify)
    //    {
    //        throw new System.NotImplementedException();
    //    }

    //    public void Initialization()
    //    {
    //        throw new System.NotImplementedException();
    //    }
    //}
}
