using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Reflection;
using System.ComponentModel;
using System.Linq;

namespace GSFramework
{
    public static class DependencyInjectionFactory
    {

        #region Parameters
        public static object GetParameterInstence(this ParameterInfo parameter, string injectionType = "")
        {
            return GetParameterInstence(parameter.ParameterType, parameter.Name, injectionType);
        }

        public static object GetPropertyInstence(this PropertyInfo parameter, string injectionType = "")
        {
            return GetParameterInstence(parameter.PropertyType, parameter.Name, injectionType);
        }

        public static object GetParameterInstence(this Type type, string name, string injectionType = "")
        {
            if (injectionType == "")
            {
                if (type.IsPrimitive)
                {
                    return Activator.CreateInstance(type);
                }
                else if (type == typeof(string))
                {
                    return "";
                }
                else
                {
                    return FrameManager.CreateInstence(type);
                }
            }
            else
            {
                switch (injectionType)
                {
                    case AppConst.Injection_Additional:
                        foreach (var tar in CreateInstenceState.GetBottomToTop())
                        {
                            if (tar is Dictionary<string, object>)
                            {
                                Dictionary<string, object> dic = tar as Dictionary<string, object>;
                                if (dic.ContainsKey(name))
                                {
                                    return dic[name];
                                }
                            }
                        }
                        return null;
                    case AppConst.Injection_InternalData:
                        return FrameManager.GetInstence<IGameData>().GetData(name);
                    case AppConst.Injection_ExternalData:
                        return FrameManager.GetData(name);
                    case AppConst.Injection_NewInstence:
                        return FrameManager.CreateInstence(type, "", name);
                    case AppConst.Injection_NewGameObject:
                        return FrameManager.GetNewGameObject(name);
                    case AppConst.Injection_GameObject:
                        return FrameManager.GetGameObject(name);
                    default:
                        return injectionType.ConvertToSimpleType(type);
                }
            }
        }
        #endregion

        #region Instence
        public static RecursiveScopeState<Dictionary<string, object>> CreateInstenceState { get; set; } = new RecursiveScopeState<Dictionary<string, object>>(new Dictionary<string, object>());

        public static object CreateInstence(this string type, Dictionary<string, object> injectionParameters)
        {
            Type t = Type.GetType(type);
            return CreateInstence(t, injectionParameters);
        }

        public static object CreateInstence(this Type type, Dictionary<string, object> injectionParameters)
        {
            if (type == null)
            {
                return null;
            }
            using (CreateInstenceState.SetScope(injectionParameters))
            {
                var ctorArray = type.GetConstructors();
                List<object> parametersList = new List<object>();
                ConstructorInfo ctor = ctorArray.Where(a => a.IsDefined(typeof(DefaultConstructorAttribute), true)).FirstOrDefault();

                if (ctor == null)
                {
                    ctor = ctorArray.OrderByDescending(c => c.GetParameters().Length).FirstOrDefault();
                    foreach (ParameterInfo parameter in ctor.GetParameters())
                    {
                        parametersList.Add(GetParameterInstence(parameter));
                    }
                }
                else
                {
                    string parametersGetMode = (Attribute.GetCustomAttribute(ctor, typeof(DefaultConstructorAttribute)) as DefaultConstructorAttribute).ParametersGetMode;
                    ParameterInfo[] constructorParameters = ctor.GetParameters();
                    if (!string.IsNullOrEmpty(parametersGetMode))
                    {
                        string[] parametersGetModes = parametersGetMode.Split(',');
                        for (int i = 0; i < constructorParameters.Length; i++)
                        {
                            parametersList.Add(GetParameterInstence(constructorParameters[i], parametersGetModes[i]));
                        }
                    }
                    else
                    {
                        foreach (ParameterInfo parameter in ctor.GetParameters())
                        {
                            parametersList.Add(GetParameterInstence(parameter));
                        }
                    }
                }
                object instence = Activator.CreateInstance(type, parametersList.ToArray());

                if (instence.GetType().GetCustomAttributes(typeof(InitializationAttributeBase)) != null)
                {
                    instence.PerformInitialization();
                }
                return instence;
            }
        }
        #endregion
    }
}