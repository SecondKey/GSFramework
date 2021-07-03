﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public class RoutedEventBindingAttribute : Attribute
    {
        public string EventToken { get; }
        public string TargetProperty { get; }
        public RoutedEventBindingAttribute(string eventToken)
        {
            EventToken = eventToken;
        }
    }
}