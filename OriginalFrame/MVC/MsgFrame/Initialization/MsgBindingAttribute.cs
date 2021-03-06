using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class MsgBindingAttribute : Attribute
    {
        public string MsgToken { get; }
        public string TargetHandler { get; }
        public MsgBindingAttribute(string eventToken, string targetHandler = AppConst.MsgHandler_Common)
        {
            MsgToken = eventToken;
            TargetHandler = targetHandler;
        }
    }
}