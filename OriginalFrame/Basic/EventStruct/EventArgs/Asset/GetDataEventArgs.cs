using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 它包含了一个 获取数据 的消息并携带所需的参数
    /// 该消息仅在BasicManager=>AssetManager过程中中有效
    /// </summary>
    public class GetDataEventArgs : EventArgs<string[]>
    {
        /// <summary> 
        /// 这个值表明数据的获取方式，例如获取一个值，获取一个数组或字典，获取一个表等
        /// </summary>
        public string GetMode { get; }
        public GetDataEventArgs(string token, string getMode, string[] parameter, object performer = null) : base(token, parameter, performer)
        {
            GetMode = getMode;
        }
    }
}