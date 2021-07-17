using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public enum VersionType
    {
        /// <summary>
        /// 长期支持版
        /// </summary>
        LTS,
        /// <summary>
        /// 稳定版
        /// </summary>
        Stable,
        /// <summary>
        /// 正式版
        /// </summary>
        Final,
        /// <summary>
        /// 测试版
        /// </summary>
        Beta,
    }
}