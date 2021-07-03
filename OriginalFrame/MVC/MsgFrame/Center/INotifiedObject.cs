using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    public delegate void SendMsgEvent(MessageArgs msg, string handler = AppConst.MsgHandler_Common, params object[] parameters);
    public delegate void RegistMsgEvent(string msg, MsgEventHandler handlerEvent, string handler);
    public delegate void UnRegistMsgEvent(string msg);
    public interface INotifiedObject : IInitializableObject
    {
        string MsgStstem { get; }

        SendMsgEvent SendMsg { get; set; }
        RegistMsgEvent RegistMsg { get; set; }
        UnRegistMsgEvent UnRegistMsg { get; set; }
    }
}