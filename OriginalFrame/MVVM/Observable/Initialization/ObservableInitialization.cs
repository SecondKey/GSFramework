using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class Initialization_ObservableAttribute : InitializationAttributeBase
    {
        public Initialization_ObservableAttribute() : base(AppConst.Init_Observable, AppConst.InitTime_Before) { }
    }
}