using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class UIView_InitializationAttribute : InitializationAttributeBase
    {
        public UIView_InitializationAttribute() : base(AppConst.Init_UIView, AppConst.InitTime_After) { }
    }
}