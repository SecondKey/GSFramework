using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class AppConst
    {
        #region Basic
        #region Path
        public static string DataPath { get { return Application.dataPath + "/"; } }

        public static string Path_Config = "Config";
        public static string Path_Root = "Root";
        public static string Path_Update = "Update";
        public static string Path_General = "General";
        public static string Path_DLC = "DLC";
        public static string Path_MOD = "MOD";
        public static string Path_Save = "Save";

        public static string Config_IOCMapping = "IOCMapping";

        public static Dictionary<string, string> Path = new Dictionary<string, string>() { { "MainConfig", DataPath + "AppConfig.xml" } };
        #endregion

        #region Parameter Get Mode
        /// <summary>
        /// 使用事件中的值
        /// </summary>
        public const string Injection_Additional = "Additional";
        /// <summary>
        /// 使用DataCenter中的值
        /// </summary>
        public const string Injection_InternalData = "Internal";
        /// <summary>
        /// 使用外部的值(调用DataCenter.GetData)
        /// </summary>
        public const string Injection_ExternalData = "External";
        /// <summary>
        /// 创建一个新的对象
        /// </summary>
        public const string Injection_NewInstence = "NewInstence";
        /// <summary>
        /// 创建一个新的游戏物体
        /// </summary>
        public const string Injection_NewGameObject = "NewGameObject";
        /// <summary>
        /// 获取一个对象，该对象可以是已经在对象池中的对象
        /// </summary>
        public const string Injection_GameObject = "GameObject";
        #endregion

        #region Initialization Mode
        public const string Init_Injection = "Injection";
        public const string Init_RoutingNode_Stack = "Init_RoutingNode_Stack";
        public const string Init_RoutingProxy = "RoutingProxy";

        public const string Init_RoutedEventBinding = "RoutedEventBinding";
        public const string Init_Observable = "Observable";
        public const string Init_MsgBinding = "MsgBinding";
        public const string Init_UIView = "UIView";
        #endregion

        #region Initialization Time
        public const string InitTime_Before = "InitMode_Before";
        public const string InitTime_After = "InitMode_After";
        #endregion

        #region ResourcesLevel
        public const string RootLevel = "Root";
        public const string GeneralLevel = "General";
        public const string AdditionalLevel = "Additional";
        public const string RunningLevel = "Running";
        public const string RunningAdditionalLevel = "RunningAdditional";
        #endregion 

        #region RoutingBlock
        public const string RoutingBlock_Resources = "RoutingBlock_Resources";
        #endregion

        #region ResourcesEvent
        public const string Resources_GetData = "Resources_GetData";
        //public const string Resources_GetBundle = "Resources_GetBundle";
        //public const string Resources_GetData = "Resources_GetData";

        public const string Resources_CreateInstence = "Resources_CreateInstence";
        public const string Resources_GetInstence = "Resources_GetInstence";
        #endregion
        #endregion

        #region MVC
        public const string MsgHandler_Common = "Common";
        public const string MsgHandler_Wait = "Wait";
        #endregion

        #region MVVM

        #endregion



        #region Other
        #region DevelopmentMode

        public const string DevelopmentModeLog_Frame = "Frame";
        public const string DevelopmentModeLog_Basic = "Frame";
        //public const string DevelopmentModeLog_ = "Frame";
        //public const string DevelopmentModeLog_Frame = "Frame";
        //public const string DevelopmentModeLog_Frame = "Frame";
        //public const string DevelopmentModeLog_Frame = "Frame";

        #endregion 
        #endregion
    }
}