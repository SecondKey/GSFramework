using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class CommandAttribute : Attribute
    {
        public string CommandName { get; }
        public string Action { get; }
        public string CanExecute { get; }

        public CommandAttribute(string commandName, string action = "", string canExecute = "")
        {
            CommandName = commandName;
            Action = action;
            CanExecute = canExecute;
        }
    }
}