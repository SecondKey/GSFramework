using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 它包含了一个 获取数据 的消息并携带所需的参数
    /// 该消息仅在BasicManager=>AssetManager过程中中有效
    /// </summary>
    public class GetDataEventArgs : BubbleEventArgs
    {
        public string GetMode { get; }

        public GetDataEventArgs(string getMode, object originalSource, object matchingParameter, params object[] parameters) : base("GetData", originalSource, MatchingStrategy.Identify, matchingParameter, parameters)
        {
            GetMode = getMode;
        }
    }
}