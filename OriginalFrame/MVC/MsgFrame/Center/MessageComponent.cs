using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GSFramework.Dev.DevelopmentModeLog;

namespace GSFramework.MVC.MSGFrame
{
    public class MsgComponent : IMsgComponent
    {
        public Dictionary<string, IMsgHandler> MsgHandlers { get; set; }
        public INotifiedObject notifiedObject { get; set; }

        public void ReceiveMessage(MessageArgs msg)
        {
            foreach (var kv in MsgHandlers.Values)
            {
                if (kv.MsgEventHandlers.ContainsKey(msg.Token))
                {
                    kv.ProcessEvent(msg);
                    return;
                }
            }
            MsgLogError($"查找{msg.Token}的执行方法时出错!在{notifiedObject}");
        }

        public void SendMsg(MessageArgs msg, string handler, object[] parameters)//发送消息方法
        {
            //msg.Source = notifiedObject;
            if (!MsgHandlers.ContainsKey(handler))
            {
                MsgHandlers.Add(handler, FrameManager.CreateInstence<IMsgHandler>(handler, null));
            }
            MsgHandlers[handler].StartOperation(msg, parameters);
        }

        public void RegistMsg(string msg, MsgEventHandler handlerEvent, string handler = AppConst.MsgHandler_Common)
        {
            if (!MsgHandlers.ContainsKey(handler))
            {
                MsgHandlers.Add(handler, FrameManager.CreateInstence<IMsgHandler>(handler, null));
            }
            if (MsgHandlers[handler].MsgEventHandlers.ContainsKey(msg))
            {
                MsgHandlers[handler].MsgEventHandlers[msg] = handlerEvent;
            }
            else
            {
                MsgHandlers[handler].MsgEventHandlers.Add(msg, handlerEvent);
            }
            FrameManager.GetInstence<MsgManager>(notifiedObject.MsgStstem).RegistMsg(this, msg);
        }

        public void UnRegistMsg(string msg)
        {
            FrameManager.GetInstence<MsgManager>(notifiedObject.MsgStstem).UnRegistMsg(this, msg);
        }
    }
}