using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework.MVC.MSGFrame
{
    public class MsgBindingInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            INotifiedObject notifiedObject = initializedObject as INotifiedObject;
            IMsgComponent component = BasicManager.Instence.GetNewObject<IMsgComponent>();
            component.notifiedObject = notifiedObject;
            component.MsgHandlers = new Dictionary<string, IMsgHandler>();

            notifiedObject.SendMsg = (msg, handler, parameters) => { component.SendMsg(msg, handler, parameters); };
            notifiedObject.RegistMsg = (msg, handlerEvent, handler) => { component.RegistMsg(msg, handlerEvent, handler); };
            notifiedObject.UnRegistMsg = (msg) => { component.UnRegistMsg(msg); };

            foreach (MethodInfo method in notifiedObject.GetType().GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(MsgBindingAttribute), true)))
            {
                MsgBindingAttribute attribute = method.GetCustomAttribute(typeof(MsgBindingAttribute)) as MsgBindingAttribute;
                component.RegistMsg(attribute.MsgToken, Delegate.CreateDelegate(typeof(MsgEventHandler), initializedObject, method) as MsgEventHandler, attribute.TargetHandler);
            }
        }
    }
}