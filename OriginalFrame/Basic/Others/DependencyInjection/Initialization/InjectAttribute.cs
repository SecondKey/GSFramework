using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class InjectAttribute : Attribute
    {
        public string ParametersGetMode { get; set; }

        public string UseCondition { get; set; }
    }
}