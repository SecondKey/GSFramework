using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using static GSFramework.Dev.DevelopmentModeLog;

namespace GSFramework.Default
{
    public class ResourceContainer : ILocalResourcesContainer
    {
        List<string> textureFormat = new List<string>() { "png", "jpg" };

        WaitState<string> waitForLoadResourceState = new WaitState<string>("", true);

        Dictionary<string, Texture> Textures = new Dictionary<string, Texture>();
        Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>();

        Dictionary<string, Sprite> Sprites = new Dictionary<string, Sprite>();

        IState<string> baseState;

        [DefaultConstructor(ParametersGetMode = AppConst.Injection_Additional)]
        public ResourceContainer(string identify)
        {
            waitForLoadResourceState.MainExcite += () => { LogParameter($"Start Load {identify} Resources"); };
            waitForLoadResourceState.MainRestore += () => { LogParameter(identify + " Resources Load Already"); baseState.RestoreState(); };
        }

        public void Load(string level, IState<string> state)
        {
            baseState = state;

            foreach (var kv in GetTextureList(level))
            {
                waitForLoadResourceState.ExciteState(kv.Value);
                RunTimeTools.Instence.StartCoroutine(CoroutineLoadTexture(kv.Key, kv.Value));
            }

            foreach (var kv in GetAudioClipList(level))
            {
                waitForLoadResourceState.ExciteState(kv.Value);
                RunTimeTools.Instence.StartCoroutine(CoroutineLoadAudio(kv.Key, kv.Value));
            }

            waitForLoadResourceState.AddAlready();
        }

        public Dictionary<string, string> GetTextureList(string level)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            switch (level)
            {
                case AppConst.ResourcesLevel_RootLevel:
                    foreach (string s in textureFormat)
                    {
                        foreach (FileInfo file in new DirectoryInfo(AppConst.Path[level]).GetFiles("*." + s))
                        {
                            tmp.Add(file.Name.Replace("." + textureFormat, ""), file.FullName);
                        }
                    }
                    break;
                case AppConst.ResourcesLevel_GeneralLevel:
                    break;
            }
            return tmp;
        }
        public Dictionary<string, string> GetAudioClipList(string level)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            switch (level)
            {
                case AppConst.ResourcesLevel_RootLevel:
                    foreach (FileInfo file in new DirectoryInfo(AppConst.Path[level]).GetFiles("*.wav"))
                    {
                        tmp.Add(file.Name.Replace(".wav", ""), file.FullName);
                    }
                    break;
                case AppConst.ResourcesLevel_GeneralLevel:
                    break;
            }
            return tmp;
        }

        IEnumerator CoroutineLoadTexture(string textureName, string texturePath)
        {
            LogParameter("加载Texture：", texturePath);
            using (UnityWebRequest request = new UnityWebRequest(texturePath))
            {
                DownloadHandlerTexture downloadHandlerTexture = new DownloadHandlerTexture(true);
                request.downloadHandler = downloadHandlerTexture;
                yield return request.SendWebRequest();
                Texture localTexture = downloadHandlerTexture.texture;
                Textures.Add(textureName, localTexture);
            }
            waitForLoadResourceState.RestoreState(texturePath);
        }

        IEnumerator CoroutineLoadAudio(string audioName, string audioPath)
        {
            LogParameter("加载Audio：", audioPath);
            using (var request = UnityWebRequestMultimedia.GetAudioClip(audioPath, AudioType.UNKNOWN))
            {
                yield return request.SendWebRequest();
                AudioClip clip = DownloadHandlerAudioClip.GetContent(request);
                AudioClips.Add(audioName, clip);
            }
            waitForLoadResourceState.RestoreState(audioPath);
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
            BasicLog("Resource:" + nam);
        }
        #endregion 

    }
}