using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GSFramework
{
    /// <summary>
    /// 测试加载，用于在调试过程中不进入加载页面直接加载内容
    /// </summary>
    public class DebugLoad
    {
        public static string OriginalSceneName;

        public static void BackToOriginalScene()
        {
            SceneManager.LoadScene(OriginalSceneName);
        }
        //public static DebugLoad instence;


        //public static bool DesignMode;

        //public static bool LoadData;
        //public static bool LoadPackage;
        //public static bool LoadStorage;

        //[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        //static void Initialize()
        //{

        //}


        //public int sceneNum;

        //public string PackageName;
        //public int StorageNum;

        //void Awake()
        //{
        //    if (DebugLoad.instence != null || GameObject.Find("Root") != null)
        //    {
        //        Destroy(this.gameObject);
        //        return;
        //    }
        //    DontDestroyOnLoad(this.gameObject);
        //    instence = this;

        //    GameData.GetInstence().ChoisePackage(PackageName);
        //    GameData.GetInstence().ChoiseStorage(StorageNum);

        //    SceneManager.LoadScene(4);
        //}
    }
}