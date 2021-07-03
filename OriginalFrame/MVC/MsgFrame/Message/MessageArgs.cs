using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 所有消息的父类
/// 通过在子类中添加参数可以传递各种参数
/// </summary>
namespace GSFramework.MVC.MSGFrame
{
    public class MessageArgs : EventArgs
    {
        public MsgSendMode SendMode { get; set; }
        public string MsgSystem { get; set; }
        public object Source { get; set; }

        public MessageArgs(string token, MsgSendMode sendMode = MsgSendMode.Auto) : base(token)
        {
            SendMode = sendMode;
        }
    }

    public class MessageArgs<T> : MessageArgs
    {
        public T Parameter { get; set; }
        public MessageArgs(string token, T parameter, MsgSendMode sendMode = MsgSendMode.Auto) : base(token, sendMode)
        {
            Parameter = parameter;
        }
    }
}