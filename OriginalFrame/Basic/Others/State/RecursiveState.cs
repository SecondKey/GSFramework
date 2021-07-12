using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 递归的状态
    /// 状态允许在已经激发的条件下，保留原本的内容后切换到下一状态。
    /// 当状态被还原时，自动回到上一状态。
    /// 递归状态是通过链表实现的
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RecursiveState<T> : IDynamicEventState<T>
    {
        protected RecursiveState<T> nextState;

        public T DefaultValue { get; set; }
        public T NowValue { get { return nextState == null ? DefaultValue : nextState.NowValue; } }
        public bool IsExcited { get { return nextState != null; } }
        /// <summary>
        /// 当前节点是否为顶层节点
        /// </summary>
        public bool IsTop { get; set; } = true;

        public Action<T> TmpExciteStateEvent { get; set; }
        public Action<T> TmpRestoreStateEvent { get; set; }

        public RecursiveState(T defaultValue)
        {
            DefaultValue = defaultValue;
        }

        public RecursiveState(T defaultValue, Action<T> changeStateEvent, Action<T> reductionStateEvent) : this(defaultValue)
        {
            TmpExciteStateEvent = changeStateEvent;
            TmpRestoreStateEvent = reductionStateEvent;
        }

        public void ExciteState(T nowValue)
        {
            if ((IsTop || nextState == null) && TmpExciteStateEvent != null)
            {
                TmpExciteStateEvent.Invoke(nowValue);
            }
            if (nextState == null)
            {
                nextState = new RecursiveState<T>(nowValue);
                nextState.IsTop = false;
            }
            else
            {
                nextState.ExciteState(nowValue);
            }
        }

        public void ExciteState(T nowValue, Action<T> changeStateEvent, Action<T> reductionStateEvent)
        {
            if ((IsTop || nextState == null) && TmpExciteStateEvent != null)
            {
                TmpExciteStateEvent.Invoke(nowValue);
            }
            if (nextState == null)
            {
                nextState = new RecursiveState<T>(nowValue, changeStateEvent, reductionStateEvent);
                nextState.IsTop = false;
            }
            else
            {
                nextState.ExciteState(nowValue);
            }
        }

        /// <summary>
        /// 通过修改底层节点的值来修改当前值
        /// 该操作不会创建新的节点
        /// </summary>
        /// <param name="nowValue"></param>
        public void SetState(T nowValue)
        {
            if (nextState == null)
            {
                DefaultValue = nowValue;
            }
            else
            {
                nextState.SetState(nowValue);
            }
        }


        public void RestoreState()
        {
            if (nextState != null && nextState.nextState == null)
            {
                if (nextState.TmpExciteStateEvent != null)
                {
                    nextState.TmpRestoreStateEvent.Invoke(nextState.NowValue);
                }
                nextState = null;
            }
            else if (nextState != null)
            {
                nextState.RestoreState();
            }
        }

        /// <summary>
        /// 获取当前递归状态的状态链
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public List<T> GetStateList(ref List<T> list)
        {
            list.Add(DefaultValue);
            if (nextState != null)
            {
                nextState.GetStateList(ref list);
            }
            return list;
        }

        /// <summary>
        /// 冒泡获取数据
        /// （从当前状态方向开始获取）
        /// </summary>
        /// <returns></returns>
        public IEnumerable GetBottomToTop()
        {
            if (nextState != null)
            {
                foreach (T value in nextState.GetBottomToTop())
                {
                    yield return value;
                }
            }
            yield return DefaultValue;
        }
    }
}