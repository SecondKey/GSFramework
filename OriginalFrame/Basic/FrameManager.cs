using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using static GSFramework.Dev.DevelopmentModeLog;

namespace GSFramework
{
    public static class FrameManager
    {
        #region FrameInstence
        static Dictionary<string, Dictionary<string, object>> FrameInstence = new Dictionary<string, Dictionary<string, object>>();
        #endregion

        #region GameInit
        public static ListState<string> GameInitializationState { get; } = new ListState<string>("Default", states: new ListStateNodeStruct<string>[]{
            new ListStateNodeStruct<string>() { StateValue = "LoadAppConfig", EnterAction = LoadAppConfig, ExitAction = () => { BasicLog("配置表加载完毕"); } },
            new ListStateNodeStruct<string>() { StateValue = "Update", EnterAction = GameUpdate, ExitAction = () => { BasicLog("资源更新完毕"); } },
            new ListStateNodeStruct<string>() { StateValue = "LoadInitResourece", EnterAction = LoadInitResources, ExitAction = ()=>{ BasicLog("初始化资源加载完成");} } },
            mainReductionStateEvent: InitOver);

        #region BeforInit
        #endregion

        static Action InitOverAction;

        /// <summary>
        /// 游戏初始化
        /// </summary>
        public static void GameInit(Action initOverAction)
        {
            BasicLog("开始游戏初始化");
            InitOverAction = initOverAction;
            GameInitializationState.ExciteState();
        }

        #region LoadAppConfig
        /// <summary>
        /// 加载配置表
        /// </summary>
        static void LoadAppConfig()
        {
            BasicLog("开始加载配置表。");
            GetInstence<ConfigManager>().LoadConfig();
            GameInitializationState.RestoreState();
        }
        #endregion

        #region Update
        /// <summary>
        /// 游戏更新
        /// </summary>
        static void GameUpdate()
        {
            BasicLog("开始更新内容。");
            GetInstence<VersionManager>().PerformUpdate();
            GameInitializationState.RestoreState();
        }

        #endregion

        #region LoadInitResources
        /// <summary>
        /// 加载初始化资源
        /// </summary>
        static void LoadInitResources()
        {
            BasicLog("开始加载初始化资源。");
            GetInstence<ResourcesManager>().LoadResources(AppConst.RootLevel);
            GameInitializationState.RestoreState();
        }
        #endregion

        /// <summary>
        /// 游戏初始化完成
        /// </summary>
        static void InitOver()
        {
            BasicLog("初始化完成!");
            if (InitOverAction != null)
            {
                InitOverAction.Invoke();
            }
        }
        #endregion

        #region Config
        public static string GetMapping(string scriptType, string scriptId)
        {
            return "";
        }
        #endregion

        #region Update
        public static void Update(IState<bool> updateState)
        {
            updateState.RestoreState();
        }
        #endregion 

        #region Resources
        #region Assets
        #region Data
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="getMode">获取对象的方式或类型</param>
        /// <param name="parameters"></param>
        public static object GetData(string getMode, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters));
        }

        public static object GetData(string getMode, string performer, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters, performer));
        }

        public static object GetData(GetDataEventArgs args)
        {
            //return resourcesManagers[AssetManager].GetData(args);
            return null;
        }
        #endregion

        #region Bundle
        public static object GetBundleResource(string getDataToken, params string[] parameters)
        {
            return null;
            //return GetBundleResource(new EventArgs(getDataToken, parameters));
        }

        public static object GetBundleResource(string getDataToken, string performer, params string[] parameters)
        {
            return null;
            //return GetBundleResource(new EventArgs(getDataToken, parameters, performer));
        }

        public static object GetBundleResource(EventArgs args)
        {
            return null;
            //return assetManager.GetBundleResource(args);
        }
        #endregion
        #endregion


        #region Instence
        #region CreateInstence

        public static T CreateInstence<T>(string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            object tmp = CreateInstence(typeof(T).FullName, id, performer, parameters);
            return (T)tmp;
        }

        public static object CreateInstence(Type baseType, string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            return CreateInstence(baseType.FullName, id, performer, parameters);
        }

        public static object CreateInstence(string baseType, string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            object tmpObject = null;
            if (performer == "")
            {
                tmpObject = GetInstence<ConfigManager>().GetMapping(baseType, id).CreateInstence(parameters);
            }
            if (tmpObject == null)
            {
                tmpObject = resourcesController.GetData(new InstenceArgs(baseType, id, performer, parameters));
            }
            if (tmpObject == null)
            {
                tmpObject = baseType.CreateInstence(parameters);
            }
            return tmpObject;
        }
        #endregion
        #region GetInstence

        public static T GetInstence<T>(string scriptId = "", string objectId = "", string layer = "")
        {
            return (T)CreateInstence(typeof(T).FullName, scriptId, objectId);
        }

        public static object GetInstence(Type baseType, string scriptId = "", string objectId = "", string layer = "")
        {
            return GetInstence(baseType.FullName, scriptId, objectId);
        }

        public static object GetInstence(string baseType, string scriptId, string layer = AppConst.RootLevel, string objectId = "")
        {
            if (layer == null)
            {
                BasicLogError("在获取实例时必须指定目标实例所在的层级");
                return null;
            }
            if (layer == AppConst.RootLevel)
            {
                if (!FrameInstence.ContainsKey(baseType))
                {
                    FrameInstence.Add(baseType, null);
                }
                if (FrameInstence[baseType].ContainsKey(scriptId))
                {
                    return FrameInstence[baseType][scriptId];
                }
                object instence = CreateInstence(baseType, scriptId, layer);
                if (instence != null)
                {
                    FrameInstence[baseType].Add(objectId, instence);
                    return instence;
                }
                else
                {
                    BasicLogError($"在获取{baseType}的实例过程中，无法创建id为：{scriptId}的对象。");
                    return null;
                }
            }
            else
            {
                return resourcesController.GetData(new TopToBottomEventArgs(AppConst.Resources_GetInstence, baseType, scriptId, objectId));
            }
        }


        #endregion
        #endregion

        #region GameObject
        #region NewGameObject
        public static GameObject GetNewGameObject(string name, string performer = "")
        {
            return null;
            //return gameObjectManager.GetNewGameObject(name);
        }
        #endregion

        #region GameObject
        public static GameObject GetGameObject(string name)
        {
            return null;
            //return gameObjectManager.GetGameObject(name);
        }
        #endregion

        #region GameObjectPool
        public static GameObject GetReusableGameObject(string name, string performer = "")
        {
            //return resourcesManagers[GameObjectManager].GetData(new ReusableGameObjectArgs(name, performer)) as GameObject;
            return null;
        }
        #endregion
        #endregion
        #endregion

    }
}