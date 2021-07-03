using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    public class CommonMsgHandler : IMsgHandler
    {
        /// <summary>
        /// 所有的操作
        /// </summary>
        public Dictionary<string, MsgEventHandler> MsgEventHandlers { get; set; } = new Dictionary<string, MsgEventHandler>();

        public void ProcessEvent(MessageArgs msg)
        {
            if (MsgEventHandlers.ContainsKey(msg.Token))
            {
                MsgEventHandlers[msg.Token].Invoke(msg);
            }
            else
            {
                DevelopmentModeLog.MsgLog($"Common中没有注册用于处理{msg.Token}的方法");
            }
        }

        public void StartOperation(MessageArgs msg, params object[] operations)
        {
            DevelopmentModeLog.MsgLog($"由Common发送了一个{msg.Token}消息");
            MsgManager.SendMsg(msg);
        }
    }
}