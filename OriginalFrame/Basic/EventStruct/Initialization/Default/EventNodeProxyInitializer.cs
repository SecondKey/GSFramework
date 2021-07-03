using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class EventNodeProxyInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            Type type = initializedObject.GetType();
            Dictionary<string, EventGetter> tmpHandlers = new Dictionary<string, EventGetter>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(EventBindingAttribute), true)))
            {
                EventBindingAttribute attribute = Attribute.GetCustomAttribute(method, typeof(EventBindingAttribute)) as EventBindingAttribute;
                string key = attribute.EventToken;

                tmpHandlers.Add(key, (EventGetter)Delegate.CreateDelegate(typeof(EventGetter), initializedObject, method));
            }
        }
    }
}