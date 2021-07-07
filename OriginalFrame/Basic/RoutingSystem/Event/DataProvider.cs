using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 数据处理器，根据事件参数计算目标值或直接返回目标值
    /// </summary>
    /// <param name="e">事件参数，可以包含事件参数，要处理的元数据或只传递令牌</param>
    /// <returns>处理后的数据</returns>
    public delegate object DataProvider(IRoutingEventArgs e);
}