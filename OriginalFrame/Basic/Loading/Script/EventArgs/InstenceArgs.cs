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

        public InstenceArgs(string scriptType, string scriptToken, string performer) : base("GetNewObject", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
        }
    }
}