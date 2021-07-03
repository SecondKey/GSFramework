using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 递归的范围状态
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecursiveScopeState<T> : RecursiveState<T>, IScopeState<T>
    {

        public RecursiveScopeState(T defaultValue) :
            base(defaultValue)
        { }
        public RecursiveScopeState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) :
            base(defaultValue, changeStateEvent, reductionStateEvent)
        { }


        public IDisposable SetScope(T nowValue)
        {
            ExciteState(nowValue);
            return this;
        }
        /// <summary>
        /// 释放时自动还原
        /// </summary>
        public void Dispose()
        {
            RestoreState();
        }
    }

}