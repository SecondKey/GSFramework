using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using static GSFramework.AppConst;

namespace GSFramework.Dev
{
    /// <summary>
    /// 可选择的输出测试
    /// </summary>
    public static class DevelopmentModeLog
    {
        public static Dictionary<string, bool> LogSwitchs = new Dictionary<string, bool>()
        {
            { DevelopmentModeLog_Frame,false},
            { DevelopmentModeLog_Basic,false},
        };


        #region Frame
        public static void FrameLog(object text)
        {
            if (LogSwitchs[DevelopmentModeLog_Frame])
            {
                Debug.Log("Frame:" + text);
            }
        }
        public static void FrameLogError(object text)
        {
            Debug.LogError("Frame错误:" + text);
        }
        #endregion 

        #region Basic
        public static void BasicLog(object text)
        {
            if (LogSwitchs[DevelopmentModeLog_Basic])
            {
                Debug.Log("Basic:" + text);
            }
        }
        public static void BasicLogError(object text)
        {
            Debug.LogError("Basic错误:" + text);
        }
        #endregion

        #region Message
        internal static bool MsgSwitch;

        public static void MsgLog(object text)
        {
            if (MsgSwitch)
            {
                Debug.Log("Message:" + text);
            }
        }

        public static void MsgLogError(object text)
        {
            Debug.LogError("Message错误:" + text);
        }
        #endregion

        #region Script
        internal static bool ScriptSwitch;

        public static void ScriptLog(object text)
        {
            if (ScriptSwitch)
            {
                Debug.Log("Message:" + text);
            }
        }
        public static void ScriptLogError(object text)
        {
            Debug.LogError("Message错误:" + text);
        }
        #endregion 

        #region Tools
        internal static bool ToolsSwitch;

        public static void ToolsLog(object text)
        {
            if (ToolsSwitch)
            {
                Debug.Log("Tools:" + text);
            }
        }
        public static void ToolsLogError(object text)
        {
            Debug.LogError("Tools错误:" + text);
        }
        #endregion

        #region UI
        internal static bool UISwitch;

        public static void UILog(object text)
        {
            if (UISwitch)
            {
                Debug.Log("UI:" + text);
            }
        }
        public static void UILogError(object text)
        {
            Debug.LogError("UI错误:" + text);
        }
        #endregion

        #region Running
        internal static bool RunningSwitch;

        public static void RunningLog(object text)
        {
            if (RunningSwitch)
            {
                Debug.Log("Running:" + text);
            }
        }

        public static void RunningLogError(object text)
        {
            Debug.LogError("Running错误:" + text);
        }
        #endregion
    }
}