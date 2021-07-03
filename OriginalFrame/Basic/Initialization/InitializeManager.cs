using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework
{
    public static class InitializeManager
    {
        static Dictionary<string, IInitializer> initializers = new Dictionary<string, IInitializer>();

        public static void PerformInitialization(this IInitializableObject initialized)
        {
            GetInitializer(AppConst.Init_Injection).Initialization(initialized);
            foreach (InitializationAttributeBase attribute in Attribute.GetCustomAttributes(initialized.GetType(), typeof(InitializationAttributeBase)).Where(p => (p as InitializationAttributeBase).InitializeTime == AppConst.InitTime_Before))
            {
                GetInitializer(attribute.InitializerType).Initialization(initialized);
            }
            if (initialized is IInitializableObjectWithMiddleFunction)
                (initialized as IInitializableObjectWithMiddleFunction).MiddleInitFunction();
            foreach (InitializationAttributeBase attribute in Attribute.GetCustomAttributes(initialized.GetType(), typeof(InitializationAttributeBase)).Where(p => (p as InitializationAttributeBase).InitializeTime == AppConst.InitTime_After))
            {
                GetInitializer(attribute.InitializerType).Initialization(initialized);
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