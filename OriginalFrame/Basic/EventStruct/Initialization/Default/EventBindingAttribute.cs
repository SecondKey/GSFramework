using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class EventBindingAttribute : Attribute
    {
        public string EventToken { get; }
        public EventBindingAttribute() { }
        public EventBindingAttribute(string eventToken)
        {
            EventToken = eventToken;
        }
    }
}