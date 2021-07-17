﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class FrameManager
    {
        #region GameInit
        public static ListState<string> GameInitializationState { get; } = new ListState<string>("Default",
            new ListStateNodeStruct<string>() { StateValue = "LoadAppConfig", EnterAction = LoadAppConfig, ExitAction = AppConfigLoadOver },
            new ListStateNodeStruct<string>() { StateValue = "Update", EnterAction = StartUpdate, ExitAction = UpdateOver },
            new ListStateNodeStruct<string>() { StateValue = "LoadInitResourece", EnterAction = LoadInitResources, ExitAction = InitResourcesLoadOver });

        #region BeforInit
        static Dictionary<object, (string id, Type type)> instenceBeforInit;
        /// <summary>
        /// 当一个类在需要在游戏初始化前创建单例时，应该使用传统的单例模式而不是FrameManager.GetInstence()。
        /// 使用该方法将目标单例注册到单例集合中，方便以后使用FrameManager.GetInstence()访问
        /// </summary>
        /// <param name="instence"></param>
        /// <param name="id"></param>
        /// <param name="type"></param>
        public static void RegistInstenceBeforInit(object instence, string id, Type type)
        {
            instenceBeforInit.Add(instence, (id, type));
        }
        #endregion

        /// <summary>
        /// 游戏初始化
        /// </summary>
        public static void GameInit()
        {
            GameInitializationState.ExciteState();
        }

        #region LoadAppConfig
        static void LoadAppConfig()
        {
            AppConfigManager.Instence.LoadConfig(AppConst.RootLevel);
            GameInitializationState.RestoreState();
        }

        static void AppConfigLoadOver()
        {

        }
        #endregion

        #region Update
        static void StartUpdate()
        {
            Debug.Log(2);
            GameInitializationState.RestoreState();
        }

        static void UpdateOver()
        {
            Debug.Log(3);
        }
        #endregion

        #region LoadInitResources
        static void LoadInitResources()
        {
            Debug.Log(4);
            GameInitializationState.RestoreState();
        }

        static void InitResourcesLoadOver()
        {
            Debug.Log(5);
        }
        #endregion
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
        /// <summary>
        /// 
        /// </summary>
        static ResourcesController resourcesController;

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
        public static RecursiveScopeState<Dictionary<string, object>> CreateInstenceState { get; set; } = new RecursiveScopeState<Dictionary<string, object>>(new Dictionary<string, object>());

        public static T CreateInstence<T>(string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            object tmp = CreateInstence(typeof(T).FullName, id, performer, parameters);
            return (T)tmp;
        }

        public static object CreateInstence(Type scriptType, string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            return CreateInstence(scriptType.FullName, id, performer, parameters);
        }

        public static object CreateInstence(string scriptType, string id = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                parameters = new Dictionary<string, object>();
            }
            object tmpObject = null;
            using (CreateInstenceState.SetScope(parameters))
            {
                if (performer == "")
                {
                    tmpObject = AppConfigManager.Instence.GetMapping(scriptType, id).CreateInstence();
                }
                if (tmpObject == null)
                {
                    tmpObject = resourcesController.GetData(new InstenceArgs(scriptType, id, performer));
                }
                if (tmpObject == null)
                {
                    tmpObject = scriptType.CreateInstence();
                }
            }
            return tmpObject;
        }

        #endregion
        #region GetInstence

        public static T GetInstence<T>(string scriptToken = "", string objectToken = "")
        {
            return (T)CreateInstence(typeof(T).FullName, scriptToken, objectToken);
        }

        public static object GetInstence(Type scriptType, string scriptToken = "", string objectToken = "")
        {
            return GetInstence(scriptType.FullName, scriptToken, objectToken);
        }

        public static object GetInstence(string scriptType, string scriptToken, string objectToken = "")
        {
            if (scriptType.GetType() == typeof(IRoutingController))
            {

            }

            return resourcesController.GetData(new TopToBottomEventArgs(AppConst.Resources_GetInstence, scriptType, scriptToken, objectToken));
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