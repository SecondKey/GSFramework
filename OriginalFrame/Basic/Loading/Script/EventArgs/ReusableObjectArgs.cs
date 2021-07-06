using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ReusableObjectArgs : EventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }
        public string Group { get; }

        public ReusableObjectArgs(string scriptType, string performer, string scriptToken, string group) : base("GetReusableObject", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
            Group = group;
        }
    }
}