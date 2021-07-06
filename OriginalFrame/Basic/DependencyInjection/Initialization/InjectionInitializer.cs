using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 依赖注入初始化器（属性注入和函数注入）
    /// 检测到对象的类型包含Init_Inject的初始化特性时，对其中所有标注了Inject的属性和方法进行注入
    /// </summary>
    public class InjectionInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            Type type = initializedObject.GetType();
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(InjectAttribute), true)))
            {
                foreach (InjectAttribute attribute in (InjectAttribute[])property.GetCustomAttributes(typeof(InjectAttribute), true))
                {
                    if (string.IsNullOrEmpty(attribute.UseCondition) || BasicManager.Instence.GetSingleton<IGameData>().CheckConditions(attribute.UseCondition))
                    {
                        if (string.IsNullOrEmpty(attribute.ParametersGetMode))
                        {
                            property.SetValue(initializedObject, property.GetPropertyInstence());
                        }
                        else
                        {
                            property.SetValue(initializedObject, property.GetPropertyInstence(attribute.ParametersGetMode));
                        }

                        break;
                    }
                }
            }
            foreach (var method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(InjectAttribute), true)))
            {
                List<object> parametersList = new List<object>();
                ParameterInfo[] parameters = method.GetParameters();

                string ParametersGetMode = (method.GetCustomAttribute(typeof(InjectAttribute)) as InjectAttribute).ParametersGetMode;
                if (string.IsNullOrEmpty(ParametersGetMode))
                {
                    string[] ParametersGetModes = ParametersGetMode.Split(',');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(parameters[i].GetParameterInstence(ParametersGetModes[i]));
                    }
                }
                else
                {
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(parameters[i].GetParameterInstence());
                    }
                }

                method.Invoke(initializedObject, parametersList.ToArray());
            }
        }
    }
}