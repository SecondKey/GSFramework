using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class Observable_InitializationAttribute : InitializationAttributeBase
    {
        public Observable_InitializationAttribute() : base(AppConst.Init_Observable, AppConst.InitTime_Before) { }
    }
}