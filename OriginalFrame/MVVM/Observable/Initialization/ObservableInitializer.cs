using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class ObservableInitializer : IInitializer
    {
        public void Initialization(IInitializableObject initializedObject)
        {
            Type type = initializedObject.GetType();
            Dictionary<string, DelegateCommand> tmpCommands = new Dictionary<string, DelegateCommand>();
            foreach (PropertyInfo property in type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(CommandAttribute), true)))
            {
                DelegateCommand targetCommand;
                if (property.GetValue(initializedObject) == null)
                {
                    targetCommand = new DelegateCommand();
                    property.SetValue(initializedObject, targetCommand);
                }
                else
                {
                    targetCommand = property.GetValue(initializedObject) as DelegateCommand;
                }
                CommandAttribute attribute = Attribute.GetCustomAttribute(property, typeof(CommandAttribute)) as CommandAttribute;
                if (!string.IsNullOrEmpty(attribute.Action))
                {
                    targetCommand.ExecuteAction = Delegate.CreateDelegate(typeof(Action<object>), initializedObject, type.GetMethod(attribute.Action)) as Action<object>;
                }

                if (!string.IsNullOrEmpty(attribute.CanExecute))
                {
                    targetCommand.CanExecuteAction = Delegate.CreateDelegate(typeof(Func<object, bool>), initializedObject, type.GetMethod(attribute.CanExecute)) as Func<object, bool>;
                }

                if (string.IsNullOrEmpty(attribute.CommandName))
                {
                    tmpCommands.Add(property.Name, property.GetValue(initializedObject) as DelegateCommand);
                }
                else
                {
                    tmpCommands.Add(attribute.CommandName, property.GetValue(initializedObject) as DelegateCommand);
                }
            }

            foreach (MethodInfo method in type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).Where(p => p.IsDefined(typeof(CommandAttribute), true)))
            {
                CommandAttribute attribute = Attribute.GetCustomAttribute(method, typeof(CommandAttribute)) as CommandAttribute;

                if (tmpCommands.ContainsKey(attribute.CommandName))
                {
                    if (method.ReturnType == typeof(void))
                    {
                        tmpCommands[attribute.CommandName].ExecuteAction = Delegate.CreateDelegate(typeof(Action<object>), initializedObject, method) as Action<object>;
                    }
                    else
                    {
                        tmpCommands[attribute.CommandName].CanExecuteAction = Delegate.CreateDelegate(typeof(Func<object, bool>), initializedObject, method) as Func<object, bool>;
                    }
                }
                else
                {
                    DelegateCommand d = new DelegateCommand();
                    if (method.ReturnType == typeof(void))
                    {
                        d.ExecuteAction = (Action<object>)Delegate.CreateDelegate(typeof(Action<object>), initializedObject, method);
                    }
                    else
                    {
                        d.CanExecuteAction = (Func<object, bool>)Delegate.CreateDelegate(typeof(Func<object, bool>), initializedObject, method);
                    }
                    tmpCommands.Add(attribute.CommandName, d);
                }
            }
            type.GetProperty("Commands").SetValue(initializedObject, tmpCommands);
        }
    }
}