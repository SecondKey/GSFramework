using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ReusableGameObjectArgs : TargetEventArgs
    {
        public string GameObjectName { get; set; }
        public ReusableGameObjectArgs(string gameObjectName, object performer = null) : base("GetReusableGameObject", performer)
        {
            GameObjectName = gameObjectName;
        }
    }
}