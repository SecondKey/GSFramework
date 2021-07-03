using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 等待状态
    /// 激活这个状态将会开启并等待一系列事件
    /// 这些事件将逐个注册到状态中
    /// 在所有事件执行完成前始终为激活状态
    /// 当所有事件全部执行完成后，状态被还原
    /// 
    /// 这个状态是通过链表实现的，每注册一个事件添加一个节点，每结束一个事件移除一个节点
    /// 当所有的节点被移除时，状态还原
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class WaitState<T>
    {
        /// <summary>
        /// 链表中的下一个事件
        /// </summary>
        protected WaitState<T> nextState;
        /// <summary>
        /// 当前节点是否时头节点
        /// </summary>
        private bool isFirst;
        /// <summary>
        /// 所有的事件是否已经添加完成
        /// </summary>
        private bool addAlready;

        /// <summary>
        /// 当前节点的标识
        /// </summary>
        public T StateToken { get; }
        /// <summary>
        /// 状态是否被激发
        /// </summary>
        public bool IsSpecial { get { return nextState != null; } }

        /// <summary>
        /// 状态被激发时执行
        /// </summary>
        public event Action MainExcite;
        /// <summary>
        /// 每注册了一个事件时执行
        /// </summary>
        public event Action<T> EachExcite;
        /// <summary>
        /// 每完成一个事件时执行
        /// </summary>
        public event Action<T> EachRestore;
        /// <summary>
        /// 状态被还原时执行
        /// </summary>
        public event Action MainRestore;

        public WaitState(T stateToken, bool isFirst = false)
        {
            this.isFirst = isFirst;
            StateToken = stateToken;
        }

        /// <summary>
        /// 激发状态（注册了一个新的事件）
        /// </summary>
        /// <param name="token"></param>
        public void ExciteState(T token)
        {
            if (isFirst)
            {
                if (nextState == null)
                {
                    if (MainExcite != null)
                    {
                        MainExcite.Invoke();
                    }
                }
                if (EachExcite != null)
                {
                    EachExcite.Invoke(token);
                }
            }

            if (nextState == null)
            {
                nextState = new WaitState<T>(token, false);
            }
            else
            {
                nextState.ExciteState(token);
            }
        }

        /// <summary>
        /// 还原状态（一个事件已完成）
        /// </summary>
        /// <param name="token"></param>
        public void RestoreState(T token)
        {
            if (nextState.StateToken as object == token as object)
            {
                nextState = nextState.nextState;

                if (isFirst)
                {
                    if (EachRestore != null)
                    {
                        EachRestore.Invoke(token);
                    }
                    if (nextState == null && addAlready)
                    {
                        MainRestore.Invoke();
                    }
                }
            }
            else
            {
                nextState.RestoreState(token);
            }
        }

        /// <summary>
        /// 所有事件全部添加完毕
        /// </summary>
        public void AddAlready()
        {
            addAlready = true;
            if (nextState == null)
            {
                MainRestore.Invoke();
            }
        }
    }
}