using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 栈式路由节点初始化器
    /// </summary>
    public class StackRoutingNodeInitializer : IInitializer
    {
        public void Initialization(object initializedObject)
        {
            Type type = initializedObject.GetType();
            Initialization_RoutingNode_StackAttribute attribute = type.GetCustomAttribute(typeof(Initialization_RoutingNode_StackAttribute)) as Initialization_RoutingNode_StackAttribute;
            var IDPropertys = type.GetProperties().Where(p => p.IsDefined(typeof(RoutingIdentifyPropertyAttribute)));

            if (IDPropertys != null && IDPropertys.Count() > 0 && IDPropertys.First().GetValue(initializedObject) != null)
            {
                BasicCenter.GetInstence<IStackRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, IDPropertys.First().GetValue(initializedObject));
            }
            else if (!attribute.IdentifyProperty.IsNullOrEmpty() & type.GetProperty(attribute.IdentifyProperty) != null && type.GetProperty(attribute.IdentifyProperty).GetValue(initializedObject) != null)
            {
                BasicCenter.GetInstence<IStackRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, type.GetProperty(attribute.IdentifyProperty).GetValue(initializedObject));
            }
            else if (attribute.Identify != null)
            {
                BasicCenter.GetInstence<IStackRoutingController>(attribute.RoutingBlockID).AddNode(initializedObject, attribute.Identify);
            }
        }
    }
}