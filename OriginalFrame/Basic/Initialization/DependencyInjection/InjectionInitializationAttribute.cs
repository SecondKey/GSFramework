using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 当一个类在使用DIF创建对象时需要进行属性注入和函数注入时，需要在类上添加该特性
    /// </summary>
    public class Injectable_InitializationAttribute : InitializationAttributeBase
    {
        public Injectable_InitializationAttribute() : base(AppConst.Init_Injection, AppConst.InitTime_Before) { }
    }
}