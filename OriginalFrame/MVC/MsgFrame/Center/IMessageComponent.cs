using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    public interface IMsgComponent
    {
        Dictionary<string, IMsgHandler> MsgHandlers { get; set; }
        INotifiedObject notifiedObject { get; set; }

        void RegistMsg(string msg, MsgEventHandler handlerEvent, string handler);
        void UnRegistMsg(string msg);
        void SendMsg(MessageArgs msg, string handler, object[] parameters);
        void ReceiveMessage(MessageArgs msg);
    }
}