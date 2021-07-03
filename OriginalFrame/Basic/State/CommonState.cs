using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 普通的状态。
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommonState<T> : IDynamicEventState<T>
    {
        public T DefaultValue { get; private set; }
        public T NowValue { get; private set; }
        public bool IsExcited { get; private set; }

        #region Event
        /// <summary>
        /// 激发事件，当事件被激发时执行
        /// </summary>
        Action<T> ExciteStateEvent;
        /// <summary>
        /// 还原事件，当事件被还原时执行
        /// </summary>
        Action<T> RestoreStateEvent;

        public Action<T> TmpExciteStateEvent { get; set; }
        public Action<T> TmpRestoreStateEvent { get; set; }
        #endregion
        public CommonState(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public CommonState(T defaultValue, Action<T> exciteStateEvent, Action<T> restoreStateEvent) : this(defaultValue)
        {
            ExciteStateEvent = exciteStateEvent;
            RestoreStateEvent = restoreStateEvent;
        }

        public void ExciteState(T nowValue)
        {
            ExciteState(nowValue, null, null);
        }

        /// <summary>
        /// 激发状态，并针对该次激发提供独特的激发事件和还原事件
        /// </summary>
        /// <param name="nowValue">激发后状态的值</param>
        /// <param name="tmpExciteStateEvent">临时激发事件</param>
        /// <param name="tmpRestoreStateEvent">临时还原事件</param>
        public void ExciteState(T nowValue, Action<T> tmpExciteStateEvent, Action<T> tmpRestoreStateEvent)
        {
            this.TmpExciteStateEvent += tmpExciteStateEvent;
            this.TmpRestoreStateEvent += tmpRestoreStateEvent;
            if (ExciteStateEvent != null)
            {
                ExciteStateEvent.Invoke(nowValue);
            }
            if (TmpExciteStateEvent != null)
            {
                TmpExciteStateEvent.Invoke(nowValue);
                TmpExciteStateEvent = null;
            }
            NowValue = nowValue;
            IsExcited = true;
        }

        public void SetState(T nowValue)
        {
            NowValue = nowValue;
            IsExcited = true;
        }

        public void RestoreState()
        {
            if (RestoreStateEvent != null)
            {
                RestoreStateEvent.Invoke(NowValue);
            }
            if (TmpRestoreStateEvent != null)
            {
                TmpRestoreStateEvent.Invoke(NowValue);
                TmpRestoreStateEvent = null;
            }
            NowValue = DefaultValue;
            IsExcited = false;
        }
    }
}


