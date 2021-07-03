using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 回传等待消息
/// </summary>
namespace GSFramework.MVC.MSGFrame
{
    public struct MsgWaitStruce
    {
        public string targetMsg;

        public INotifiedObject waitObject;

        public bool complete;
    }

    public class MsgWait : MessageArgs<MsgWaitStruce>
    {
        public MsgWait(string token, MsgWaitStruce systemParameter, MsgSendMode sendMode = MsgSendMode.Auto) : base(token, systemParameter, sendMode) { }
    }
}