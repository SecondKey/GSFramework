using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由代理初始化器
    /// </summary>
    public class RoutingProxyInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            Type type = initializedObject.GetType();
            Dictionary<string, DataProvider> tmpHandlers = new Dictionary<string, DataProvider>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(EventBindingAttribute), true)))
            {
                EventBindingAttribute attribute = Attribute.GetCustomAttribute(method, typeof(EventBindingAttribute)) as EventBindingAttribute;
                string key = string.IsNullOrEmpty(attribute.EventToken) ? attribute.EventToken : method.Name;
                if (method.ReturnType == typeof(void))
                {

                }
                else
                {
                    
                }

                initializedObject as IRoutedNodeProxy
                tmpHandlers.Add(key, (DataProvider)Delegate.CreateDelegate(typeof(DataProvider), initializedObject, method));
            }
        }
    }
}