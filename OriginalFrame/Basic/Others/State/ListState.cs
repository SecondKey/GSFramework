using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static GSFramework.Dev.DevelopmentModeLog;

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
        List<ListStateNodeStruct<T>> stateList = new List<ListStateNodeStruct<T>>();

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

        #region Event
        /// <summary>
        /// 默认的激发事件，在状态被激发时执行
        /// </summary>
        Action<T> ExciteStateEvent { get; }
        /// <summary>
        /// 默认的还原事件，在状态被还原时执行
        /// </summary>
        Action<T> ReductionStateEvent { get; }

        /// <summary>
        /// 临时的激发事件，在状态被激发时执行，执行一次后清空
        /// </summary>
        public Action<T> TmpExciteStateEvent { private get; set; }
        /// <summary>
        /// 临时的还原事件，在状态被还原时执行，执行一个后清空
        /// </summary>
        public Action<T> TmpReductionStateEvent { private get; set; }

        /// <summary>
        /// 主激发事件，仅在状态从默认状态转为激发状态时执行
        /// </summary>
        Action MainExciteStateEvent { get; }
        /// <summary>
        /// 主还原事件，仅在状态从激发状态转为默认状态时执行
        /// </summary>
        Action MainReductionStateEvent { get; }
        #endregion

        public ListState(T defalutValue, ListStateNodeStruct<T>[] states = null, Action mainExciteStateEvent = null, Action mainReductionStateEvent = null, Action<T> exciteStateEvent = null, Action<T> reductionStateEvent = null)
        {
            DefaultValue = defalutValue;
            MainExciteStateEvent = mainExciteStateEvent;
            MainReductionStateEvent = mainReductionStateEvent;
            ExciteStateEvent = exciteStateEvent;
            ReductionStateEvent = reductionStateEvent;

            if (states == null)
            {
                stateList = new List<ListStateNodeStruct<T>>();
            }
            else
            {
                stateList = new List<ListStateNodeStruct<T>>(states);
            }
        }

        #region State
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
            if (stateList == null)
                stateList = new List<ListStateNodeStruct<T>>();
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
        #endregion

        #region ExciteAndREstore
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
                FrameLog("");

                nowStep = index;
                NowValue = stateList[index].StateValue;

                if (MainExciteStateEvent != null)
                {
                    MainExciteStateEvent.Invoke();
                }
                if (ExciteStateEvent != null)
                {
                    ExciteStateEvent.Invoke(NowValue);
                }
                if (stateList[nowStep].EnterAction != null)
                {
                    stateList[nowStep].EnterAction.Invoke();
                }
            }
            else
            {

            }
        }

        /// <summary>
        /// 还原事件
        /// </summary>
        public void RestoreState()
        {
            if (stateList[nowStep].ExitAction != null)//如果当前状态的退出函数不为空
            {
                stateList[nowStep].ExitAction.Invoke();//执行退出函数
            }
            if (TmpReductionStateEvent != null)//如果临时还原函数不为空
            {
                TmpReductionStateEvent.Invoke(NowValue);//执行临时还原函数
                TmpReductionStateEvent = null;//将临时还原函数置空
            }
            if (ReductionStateEvent != null)//如果公共还原函数不为空
            {
                ReductionStateEvent.Invoke(NowValue);//执行公共还原函数
            }
            nowStep += 1;
            if (nowStep >= stateList.Count)//当所有状态全部还原完毕
            {
                if (MainReductionStateEvent != null)//如果主还原函数不为空
                {
                    MainReductionStateEvent.Invoke();//执行主还原函数
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

        #endregion

        #region Tools
        int GetIndexByValue(T value)
        {
            return stateList.IndexOf(stateList.Where(p => p.StateValue.Equals(value)).First());
        }
        #endregion 
    }
}