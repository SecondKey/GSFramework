using UnityEngine;
using System;
using System.Collections.Generic;

namespace GSFramework
{
    /// <summary>
    /// 标准的状态
    /// </summary>
    /// <typeparam name="T">状态中的值的类型</typeparam>
    public interface IState<T>
    {
        /// <summary>
        /// 状态的默认值
        /// </summary>
        T DefaultValue { get; }
        /// <summary>
        /// 状态当前的值
        /// </summary>
        T NowValue { get; }
        /// <summary>
        /// 状态是否处于激发态
        /// </summary>
        bool IsExcited { get; }

        /// <summary>
        /// 激发状态
        /// </summary>
        /// <param name="nowValue">激发后的状态值</param>
        void ExciteState(T nowValue);
        /// <summary>
        /// 将状态还原回默认状态
        /// </summary>
        void RestoreState();
    }
}