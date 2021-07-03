using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class EventNodeInitializationAttribute : InitializationAttributeBase
    {
        public EventNodeInitializationAttribute() : base(AppConst.Init_EventNodeProxy, AppConst.InitTime_Before) { }
    }
}