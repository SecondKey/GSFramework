using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    public static class InitializeManager
    {
        static Dictionary<string, IInitializer> initializers = new Dictionary<string, IInitializer>();

        public static void PerformInitialization(this object initializableObject)
        {
            GetInitializer(AppConst.Init_Injection).Initialization(initializableObject);
            foreach (MethodInfo info in initializableObject.GetType().GetMethods().Where(p => p.IsDefined(typeof(InitStartFunctionAttribute))))
            {
                info.Invoke(initializableObject, null);
            }
            foreach (InitializationAttributeBase attribute in Attribute.GetCustomAttributes(initializableObject.GetType(), typeof(InitializationAttributeBase)).Where(p => (p as InitializationAttributeBase).InitializeTime == AppConst.InitTime_Before))
            {
                GetInitializer(attribute.InitializerType).Initialization(initializableObject);
            }
            foreach (MethodInfo info in initializableObject.GetType().GetMethods().Where(p => p.IsDefined(typeof(InitMiddleFunctionAttribute))))
            {
                info.Invoke(initializableObject, null);
            }
            foreach (InitializationAttributeBase attribute in Attribute.GetCustomAttributes(initializableObject.GetType(), typeof(InitializationAttributeBase)).Where(p => (p as InitializationAttributeBase).InitializeTime == AppConst.InitTime_After))
            {
                GetInitializer(attribute.InitializerType).Initialization(initializableObject);
            }
            foreach (MethodInfo info in initializableObject.GetType().GetMethods().Where(p => p.IsDefined(typeof(InitEndFunctionAttribute))))
            {
                info.Invoke(initializableObject, null);
            }
        }

        static IInitializer GetInitializer(string initializerName)
        {
            if (!initializers.ContainsKey(initializerName))
            {
                initializers.Add(initializerName, BasicManager.Instence.GetNewObject<IInitializer>(initializerName));
            }
            return initializers[initializerName];
        }
    }
}