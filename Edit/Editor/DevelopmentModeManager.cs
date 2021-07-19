using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using static GSFramework.Dev.DevelopmentModeLog;
using static GSFramework.AppConst;

namespace GSFramework.Dev
{
    public class DevelopmentModeManager
    {
        #region 变量
        const string DevelopmentMode = "工具/开发模式/开发模式";

        const string DataLoadPath = "工具/开发模式/加载/数据";
        const string PackageLoadPath = "工具/开发模式/加载/资源包";
        const string StorageLoadPath = "工具/开发模式/加载/存档";


        const string FramePath = "工具/开发模式/输出/Frame";
        const string BasicPath = "工具/开发模式/输出/Basic";

        const string ScriptPath = "工具/开发模式/输出/Script";

        const string MsgPath = "工具/开发模式/输出/Msg";
        const string ToolsPath = "工具/开发模式/输出/Tools";
        const string UIPath = "工具/开发模式/输出/UI";
        const string RunningPath = "工具/开发模式/输出/Running";
        #endregion



        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void SimulatedLoad()
        {
            LogSwitchs[DevelopmentModeLog_Frame] = Menu.GetChecked(FramePath);
            LogSwitchs[DevelopmentModeLog_Basic] = Menu.GetChecked(BasicPath);
            //DevelopmentModeLog.MsgDebug = Menu.GetChecked(MsgPath);
            //DevelopmentModeLog.ScriptDebug = Menu.GetChecked(ScriptPath);
            //DevelopmentModeLog.ToolsDebug = Menu.GetChecked(ToolsPath);
            //DevelopmentModeLog.UIDebug = Menu.GetChecked(UIPath);
            //DevelopmentModeLog.RunningDebug = Menu.GetChecked(RunningPath);

            if (Menu.GetChecked(DevelopmentMode))
            {
                string startSceneName = "SimulatedInitializationScene";
                Scene scene = SceneManager.GetActiveScene();
                if (scene.name.Equals(startSceneName))
                {
                    return;
                }
                DebugLoad.OriginalSceneName = scene.name;
                SceneManager.LoadScene(startSceneName);
            }
        }

        public static void SimulatedLoadOver()
        {

        }

        [MenuItem(DevelopmentMode, false, 0)]
        public static void SetDevelopmentMode()
        {
            Menu.SetChecked(DevelopmentMode, !Menu.GetChecked(DevelopmentMode));
            Debug.Log("DevelopmentMode:" + (Menu.GetChecked(DevelopmentMode) ? "开启" : "关闭"));
        }

        #region Log
        [MenuItem(FramePath)]
        public static void FrameDebug()
        {
            Menu.SetChecked(FramePath, !Menu.GetChecked(FramePath));
            Debug.Log("Frame:" + Menu.GetChecked(FramePath));
        }

        [MenuItem(BasicPath)]
        public static void BasicDebug()
        {
            Menu.SetChecked(BasicPath, !Menu.GetChecked(BasicPath));
            Debug.Log("Basic:" + Menu.GetChecked(BasicPath));
        }




        [MenuItem(MsgPath)]
        public static void MsgDebug()
        {
            Menu.SetChecked(MsgPath, !Menu.GetChecked(MsgPath));
            Debug.Log("Msg:" + Menu.GetChecked(MsgPath));
        }

        [MenuItem(ScriptPath)]
        public static void ScriptDebug()
        {
            Menu.SetChecked(ScriptPath, !Menu.GetChecked(ScriptPath));
            Debug.Log("Msg:" + Menu.GetChecked(ScriptPath));
        }





        [MenuItem(ToolsPath)]
        public static void ToolsDebug()
        {
            Menu.SetChecked(ToolsPath, !Menu.GetChecked(ToolsPath));
            Debug.Log("Tools:" + Menu.GetChecked(ToolsPath));
        }
        [MenuItem(UIPath)]
        public static void UIDebug()
        {
            Menu.SetChecked(UIPath, !Menu.GetChecked(UIPath));
            Debug.Log("UI:" + Menu.GetChecked(UIPath));
        }
        [MenuItem(RunningPath)]
        public static void RunningDebug()
        {
            Menu.SetChecked(RunningPath, !Menu.GetChecked(RunningPath));
            Debug.Log("Running:" + Menu.GetChecked(RunningPath));
        }
        #endregion

        #region Load

        [MenuItem(DataLoadPath)]
        public static void LoadData()
        {
            Menu.SetChecked(DataLoadPath, true);
            Menu.SetChecked(PackageLoadPath, false);
        }

        [MenuItem(PackageLoadPath)]
        public static void LoadPackage()
        {
            Menu.SetChecked(DataLoadPath, true);
            Menu.SetChecked(PackageLoadPath, true);
        }

        [MenuItem(StorageLoadPath)]
        public static void LoadStorage()
        {
            Menu.SetChecked(StorageLoadPath, !Menu.GetChecked(StorageLoadPath));
        }

        #endregion
    }
}