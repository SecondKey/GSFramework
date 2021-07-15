using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GSFramework
{
    public struct ListStateNodeStruct<T>
    {
        public T StateValue;
        public Action EnterAction;
        public Action ExitAction;
    }

    /// <summary>
    /// 链式的自动状态，储存一个状态链并自动切换,执行所有的状态后切换回默认状态
    /// </summary>
    /// <typeparam name="T">状态标识的类型</typeparam>
    public class ListState<T> : IState<T>
    {
        public List<ListStateNodeStruct<T>> stateList = new List<ListStateNodeStruct<T>>();

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
        }

        public ListState(T defalutValue, params ListStateNodeStruct<T>[] states) : this(defalutValue)
        {
            stateList = new List<ListStateNodeStruct<T>>(states);
        }

        public ListState(T defalutValue, Action<T> changeStateEvent, Action<T> reductionStateEvent, params ListStateNodeStruct<T>[] states) : this(defalutValue, states)
        {
            ChangeStateEvent = changeStateEvent;
            ReductionStateEvent = reductionStateEvent;
        }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param name="value">状态值</param>
        /// <param name="enterAction">状态激发事件</param>
        /// <param name="exitAction">状态还原事件</param>
        public void AddState(T value, Action enterAction, Action exitAction)
        {
            AddState(new ListStateNodeStruct<T>() { StateValue = value, EnterAction = enterAction, ExitAction = exitAction });
        }

        /// <summary>
        /// 添加一个状态
        /// </summary>
        /// <param name="state">目标状态</param>
        public void AddState(ListStateNodeStruct<T> state)
        {
            stateList.Add(state);
        }

        /// <summary>
        /// 在指定状态后插入一个状态
        /// </summary>
        /// <param name="value">前一个状态值</param>
        /// <param name="state">插入状态</param>
        public void InsertState(T value, ListStateNodeStruct<T> state)
        {
            int index = stateList.IndexOf(stateList.Where(p => p.StateValue.Equals(value)).First());
            InsertState(index, state);
        }

        /// <summary>
        /// 在指定位置插入一个状态
        /// </summary>
        /// <param name="index">目标位置</param>
        /// <param name="state">插入状态</param>
        public void InsertState(int index, ListStateNodeStruct<T> state)
        {
            stateList.Insert(index, state);
        }
        /// <summary>
        /// 移除一个状态
        /// </summary>
        /// <param name="index">目标位置</param>
        public void RemoveState(int index)
        {
            stateList.RemoveAt(index);
        }

        /// <summary>
        /// 移除一个状态
        /// </summary>
        /// <param name="value">目标值</param>
        public void RemoveState(T value)
        {
            var node = stateList.Where(p => p.StateValue.Equals(value)).First();
            stateList.Remove(node);
        }

        public void ExciteState()
        {
            ExciteState(0);
        }

        public void ExciteState(T nowValue)
        {
            ExciteState(GetIndexByValue(nowValue));
        }

        public void ExciteState(int index)
        {
            if (!IsExcited)
            {
                nowStep = index;
                NowValue = stateList[index].StateValue;

                if (ChangeStateEvent != null)
                {
                    ChangeStateEvent.Invoke(NowValue);
                }
                if (stateList[nowStep].EnterAction != null)
                {
                    stateList[nowStep].EnterAction.Invoke();
                }
            }
        }

        public void RestoreState()
        {
            if (stateList[nowStep].ExitAction != null)
            {
                stateList[nowStep].ExitAction.Invoke();
            }
            nowStep += 1;
            if (nowStep >= stateList.Count)
            {
                if (TmpReductionStateEvent != null)
                {
                    TmpReductionStateEvent.Invoke(NowValue);
                    TmpReductionStateEvent = null;
                }
                if (ReductionStateEvent != null)
                {
                    ReductionStateEvent.Invoke(NowValue);
                }
                nowStep = -1;
                NowValue = DefaultValue;
            }
            else
            {
                if (stateList[nowStep].EnterAction != null)
                {
                    stateList[nowStep].EnterAction.Invoke();
                }
            }
        }

        int GetIndexByValue(T value)
        {
            return stateList.IndexOf(stateList.Where(p => p.StateValue.Equals(value)).First());
        }
    }
}