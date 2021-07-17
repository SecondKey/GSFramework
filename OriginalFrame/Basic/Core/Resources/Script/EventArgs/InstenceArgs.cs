using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class InstenceArgs : TargetEventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }
        public Dictionary<string, object> InjectionParameters { get; }

        public InstenceArgs(string scriptType, string scriptToken, string performer, Dictionary<string, object> injectionParameters) : base("GetNewObject", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
            InjectionParameters = injectionParameters;
        }
    }
}