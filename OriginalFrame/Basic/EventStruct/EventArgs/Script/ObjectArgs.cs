using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ObjectArgs : EventArgs
    {
        public string ScriptType { get; }
        public string ScriptToken { get; }
        public string ObjectToken { get; }

        public ObjectArgs(string scriptType, string scriptToken, string objectToken, string performer) : base("GetObject", performer)
        {
            ScriptType = scriptType;
            ScriptToken = scriptToken;
            ObjectToken = objectToken;
        }
    }
}