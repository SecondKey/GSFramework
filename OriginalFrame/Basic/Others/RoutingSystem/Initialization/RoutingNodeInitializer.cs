using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 路由节点初始化器
    /// </summary>
    public class RoutingNodeInitializer : IInitializer
    {
        public void Initialization(object initializedObject)
        {
            Type type = initializedObject.GetType();
            Initialization_RoutingNodeAttribute attribute = type.GetCustomAttribute(typeof(Initialization_RoutingNodeAttribute)) as Initialization_RoutingNodeAttribute;
            var IDPropertys = type.GetProperties().Where(p => p.IsDefined(typeof(RoutingIdentifyPropertyAttribute)));

            if (IDPropertys != null && IDPropertys.Count() > 0 && IDPropertys.First().GetValue(initializedObject) != null)
            {
                BasicCenter.GetInstence<IRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, IDPropertys.First().GetValue(initializedObject));
            }
            else if (!attribute.IdentifyProperty.IsNullOrEmpty() & type.GetProperty(attribute.IdentifyProperty) != null && type.GetProperty(attribute.IdentifyProperty).GetValue(initializedObject) != null)
            {
                BasicCenter.GetInstence<IRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, type.GetProperty(attribute.IdentifyProperty).GetValue(initializedObject));
            }
            else if (attribute.Identify != null)
            {
                BasicCenter.GetInstence<IRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, attribute.Identify);
            }
        }
    }
}