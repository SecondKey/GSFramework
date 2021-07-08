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
                    return BasicManager.Instence.GetNewObject(type);
                }
            }
            else
            {
                switch (injectionType)
                {
                    case AppConst.Injection_Static:
                        return injectionType.Replace("Static_", "").ConvertToSimpleType(type);
                    case AppConst.Injection_Additional:
                        foreach (var tar in BasicManager.Instence.CreateInstenceState.GetBottomToTop())
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
                        return BasicManager.Instence.GetSingleton<IGameData>().GetData(name);
                    case AppConst.Injection_ExternalData:
                        return BasicManager.Instence.GetData(name);
                    case AppConst.Injection_NewInstence:
                        return BasicManager.Instence.GetNewObject(type, "", name);
                    case AppConst.Injection_NewGameObject:
                        return BasicManager.Instence.GetNewGameObject(name);
                    case AppConst.Injection_GameObject:
                        return BasicManager.Instence.GetGameObject(name);
                    default:
                        return injectionType.ConvertToSimpleType(type);
                }
            }
        }
        #endregion

        #region Instence
        public static object CreateInstence(this string type)
        {
            Type t = Type.GetType(type);
            var ctorArray = t.GetConstructors();
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
                ParameterInfo[] parameters = ctor.GetParameters();
                if (!string.IsNullOrEmpty(parametersGetMode))
                {
                    string[] parametersGetModes = parametersGetMode.Split(',');
                    for (int i = 0; i < parameters.Length; i++)
                    {
                        parametersList.Add(GetParameterInstence(parameters[i], parametersGetModes[i]));
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
            object instence = Activator.CreateInstance(t, parametersList.ToArray());
            if (instence.GetType().GetCustomAttributes(typeof(InitializationAttributeBase)) != null)
            {
                instence.PerformInitialization();
            }
            return instence;
        }

        public static object CreateInstence(this Type type)
        {
            if (type == null)
            {
                return null;
            }
            return CreateInstence(type.FullName);
        }
        #endregion
    }
}