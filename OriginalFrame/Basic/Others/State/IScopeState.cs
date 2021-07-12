using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 范围状态，可以使用using限制特殊状态的作用域
    /// 离开作用域将自动结束特殊状态，或手动退出特殊状态
    /// 在作用域中对状态进行任何修改，在离开作用域会立即还原，并执行退出方法
    /// </summary>
    public interface IScopeState<T> : IDynamicEventState<T>, IDisposable
    {
        /// <summary>
        /// 在IScopeState中不推荐使用ExciteState激发状体
        /// 使用using(IScopeState.SetScope(nowValue)){...}来设置范围
        /// 状态将在退出范围后自动还原并释放
        /// </summary>
        /// <param name="nowValue">激发后的状态值</param>
        /// <returns>用于限定范围的状态本身</returns>
        IDisposable SetScope(T nowValue);
    }
}