using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 携带动态事件的状态，允许添加动态的激发事件和还原事件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDynamicEventState<T> : IState<T>
    {
        /// <summary>
        /// 临时的激发事件，在状态被激发时执行，执行后清空
        /// </summary>
        Action<T> TmpExciteStateEvent { get; set; }
        /// <summary>
        /// 临时的还原事件，在状态还原发时执行，执行后清空
        /// </summary>
        Action<T> TmpRestoreStateEvent { get; set; }
        /// <summary>
        /// 设置当前状态
        /// 这个方法不会触发激发事件，但如果状态被激发，还原时仍会执行还原状态
        /// </summary>
        /// <param name="nowValue"></param>
        void SetState(T nowValue);
    }
}