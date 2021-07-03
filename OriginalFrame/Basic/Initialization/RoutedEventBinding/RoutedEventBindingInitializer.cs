using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public class RoutedEventBindingInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            Type type = initializedObject.GetType();
            string propertyName = "RoutedHandlers";
            Dictionary<string, EventHandler> tmpHandlers = new Dictionary<string, EventHandler>();
            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(RoutedEventBindingAttribute), true)))
            {
                RoutedEventBindingAttribute attribute = Attribute.GetCustomAttribute(method, typeof(RoutedEventBindingAttribute)) as RoutedEventBindingAttribute;
                string key = attribute.EventToken;
                if (!string.IsNullOrEmpty(attribute.TargetProperty))
                {
                    propertyName = attribute.TargetProperty;
                }
                tmpHandlers.Add(key, (EventHandler)Delegate.CreateDelegate(typeof(EventHandler), initializedObject, method));
            }

            PropertyInfo property = type.GetProperty(propertyName);
            if (property != null)
            {
                property.SetValue(initializedObject, tmpHandlers);
            }
        }
    }
}