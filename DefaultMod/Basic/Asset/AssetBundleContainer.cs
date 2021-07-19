using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using static GSFramework.Dev.DevelopmentModeLog;

namespace GSFramework.Default
{
    public class AssetBundleContainer : IBundleContainer
    {
        WaitState<string> waitForLoadBundleState = new WaitState<string>("", true);

        Dictionary<string, AssetBundle> AssetBundles = new Dictionary<string, AssetBundle>();

        IState<string> baseState;

        [DefaultConstructor(ParametersGetMode = AppConst.Injection_Additional)]
        public AssetBundleContainer(string identify)
        {
            waitForLoadBundleState.MainExcite += () => { LogParameter($"Start Load {identify} Bundle"); };
            waitForLoadBundleState.MainRestore += () => { LogParameter($"{identify} Bundle Load Already"); baseState.RestoreState(); };
        }

        public void Load(string level, IState<string> state)
        {
            baseState = state;

            Dictionary<string, string> bundles = new Dictionary<string, string>();
            switch (level)
            {
                case AppConst.RootLevel:
                    foreach (FileInfo file in new DirectoryInfo(AppConst.Path[level]).GetFiles("*.u3d"))
                    {
                        bundles.Add(file.Name.Replace(".u3d", ""), file.FullName);
                    }
                    break;
                case AppConst.GeneralLevel:
                    break;
            }

            foreach (var kv in bundles)
            {
                waitForLoadBundleState.ExciteState(kv.Value);
                RunTimeTools.Instence.StartCoroutine(CoroutineLoadBundle(kv.Key, kv.Value));
            }
            waitForLoadBundleState.AddAlready();
        }

        IEnumerator CoroutineLoadBundle(string bundleName, string bundlePath)
        {
            LogParameter("加载AssetBundle：", bundlePath);
            AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return request;
            AssetBundle ab = request.assetBundle;
            AssetBundles.Add(bundleName, ab);
            waitForLoadBundleState.RestoreState(bundlePath);
        }

        #region Tools
        /// <summary>
        /// 输出每一次访问传入的参数
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="parameter"></param>
        public void LogParameter(string nam, params string[] parameter)
        {
            for (int i = 0; i < parameter.Length; i++)
            {
                nam = nam + " " + parameter[i];
            }
            BasicLog("AssetBundle:" + nam);
        }
        #endregion 

    }
}