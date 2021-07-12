using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 链式的自动状态，储存一个状态链并自动切换,执行所有的状态后切换回默认状态
    /// </summary>
    /// <typeparam name="T">状态标识的类型</typeparam>
    public class ListState<T> : IState<T>
    {
        public List<KeyValuePair<Action<T>, Action>> stateList;

        public T DefaultValue { get; private set; }
        public T NowValue { get; private set; }
        /// <summary>
        /// 从第一个状态开始到最后一个状态结束，IsSpecial保持为True
        /// </summary>
        public bool IsExcited { get { return nowStep != -1; } }
        /// <summary>
        /// 当前的状态在状态链中的位置
        /// -1代表处于默认状态
        /// </summary>
        int nowStep = -1;

        /// <summary>
        /// 默认的激发事件，在状态被激发时执行
        /// </summary>
        Action<T> ChangeStateEvent;
        /// <summary>
        /// 默认的还原事件，在状态被还原时执行
        /// </summary>
        Action<T> ReductionStateEvent;

        /// <summary>
        /// 临时的激发事件，在状态被激发时执行，执行一次后清空
        /// </summary>
        public Action<T> TmpChangeStateEvent;
        /// <summary>
        /// 临时的还原事件，在状态被还原时执行，执行一个后清空
        /// </summary>
        public Action<T> TmpReductionStateEvent;

        public ListState(T defalutValue)
        {
            DefaultValue = defalutValue;
            stateList = new List<KeyValuePair<Action<T>, Action>>();
        }

        public ListState(T defalutValue, Action<T> changeStateEvent, Action<T> reductionStateEvent, params KeyValuePair<Action<T>, Action>[] states) : this(defalutValue)
        {
            stateList = new List<KeyValuePair<Action<T>, Action>>(states);
            ChangeStateEvent = changeStateEvent;
            ReductionStateEvent = reductionStateEvent;
        }

        /// <summary>
        /// 添加一个仅有激发事件的状态
        /// </summary>
        /// <param name="changeStateAction"></param>
        public void AddState(Action<T> exciteStateEvent)
        {
            AddState(exciteStateEvent, null);
        }

        /// <summary>
        /// 添加一个包含激发事件和还原事件的状态
        /// </summary>
        /// <param name="changeStateAction"></param>
        /// <param name="reductionStateAction"></param>
        public void AddState(Action<T> exciteStateEvent, Action reductionStateAction)
        {
            AddState(new KeyValuePair<Action<T>, Action>(exciteStateEvent, reductionStateAction));
        }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param name="state"></param>
        public void AddState(KeyValuePair<Action<T>, Action> state)
        {
            stateList.Add(state);
        }

        /// <summary>
        /// 在指定位置插入一个状态
        /// </summary>
        /// <param name="state">状态</param>
        /// <param name="index">目标位置</param>
        public void InsertState(KeyValuePair<Action<T>, Action> state, int index)
        {
            stateList.Insert(index, state);
        }

        public void ExciteState(T nowValue)
        {
            if (!IsExcited)
            {
                if (ChangeStateEvent != null)
                {
                    ChangeStateEvent.Invoke(nowValue);
                }
                NowValue = nowValue;
                nowStep = 0;
                if (stateList[nowStep].Key != null)
                {
                    stateList[nowStep].Key.Invoke(nowValue);
                }
            }
        }

        public void RestoreState()
        {
            if (stateList[nowStep].Value != null)
            {
                stateList[nowStep].Value.Invoke();
            }
            nowStep += 1;
            if (nowStep >= stateList.Count)
            {
                nowStep = -1;
                T tmpValue = NowValue;
                NowValue = DefaultValue;
                if (TmpReductionStateEvent != null)
                {
                    TmpReductionStateEvent.Invoke(tmpValue);
                    TmpReductionStateEvent = null;
                }
                if (ReductionStateEvent != null)
                {
                    ReductionStateEvent.Invoke(tmpValue);
                }
            }
            else
            {
                if (stateList[nowStep].Key != null)
                {
                    stateList[nowStep].Key.Invoke(NowValue);
                }
            }
        }
    }
}