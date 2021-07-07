using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class Initialization_UIViewAttribute : InitializationAttributeBase
    {
        public Initialization_UIViewAttribute() : base(AppConst.Init_UIView, AppConst.InitTime_After) { }
    }
}