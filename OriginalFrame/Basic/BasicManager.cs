using GSFramework.MVC;
using System;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using static GSFramework.AppConst;
using static GSFramework.DevelopmentModeLog;

namespace GSFramework
{
    /// <summary>
    /// 底层管理类
    /// 从实际来说Baisc管理游戏底层的数据和资源
    /// 这个类负责对这些数据和资源惊醒统一的管理和调度
    /// </summary>
    public class BasicManager
    {
        #region 单例
        private static BasicManager instence;
        private BasicManager() { }
        public static BasicManager Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new BasicManager();
                }
                return instence;
            }
        }

        #endregion

        #region GameInitalization
        Action onInitalizationOver;
        public CommonState<bool> UpdateState { get; private set; } = new CommonState<bool>(false);
        /// <summary>
        /// 进行游戏初始化，包括读取配置文件，检查更新以及初始资源的加载
        /// </summary>
        public void GameInitalization()
        {
            GameInitalization(() => { });
        }
        /// <summary>
        /// 进行游戏初始化，包括读取配置文件，检查更新以及初始资源的加载
        /// </summary>
        /// <param name="initalizationOverAction">初始化结束后的回调</param>
        public void GameInitalization(Action initalizationOverAction)
        {
            //onInitalizationOver = initalizationOverAction;
            //foreach (string m in AppConfig.Instence.ManagerList)
            //{
            //    IResourcesManager manager = GetNewObject<IResourcesManager>(m, "", new Dictionary<string, object>() { { "Identify", RootLevel } });
            //    //AppConfig.Instence.GetMapping(typeof(IResourcesManager).FullName, m).CreateInstence() as IResourcesManager;
            //    resourcesManagers.Add(m, manager);
            //    //loadResourcesState.AddState((s) => { manager.HandleEvent(new EventArgs("Load", loadResourcesState, s)); });
            //}
            //loadResourcesState.TmpReductionStateEvent += (s) => { StartUpdate(); };
            //loadResourcesState.ExciteState(RootLevel);
        }

        /// <summary>
        /// 初始化结束
        /// </summary>
        void InitalizationOver()
        {
            BasicLog("InitalizationOver");
            if (onInitalizationOver != null)
            {
                onInitalizationOver.Invoke();
            }
        }
        #endregion

        #region ResourcesManagement
        #region Load

        #endregion

        #region Get
        #region Asset
        #region Data
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="getMode">获取对象的方式或类型</param>
        /// <param name="parameters"></param>
        public object GetData(string getMode, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters));
        }

        public object GetData(string getMode, string performer, params string[] parameters)
        {
            return GetData(new GetDataEventArgs("GetData", getMode, parameters, performer));
        }

        object GetData(GetDataEventArgs args)
        {
            //return resourcesManagers[AssetManager].GetData(args);
            return null;
        }
        #endregion

        #region Bundle
        public object GetBundleResource(string getDataToken, params string[] parameters)
        {
            return null;
            //return GetBundleResource(new EventArgs(getDataToken, parameters));
        }

        public object GetBundleResource(string getDataToken, string performer, params string[] parameters)
        {
            return null;
            //return GetBundleResource(new EventArgs(getDataToken, parameters, performer));
        }

        object GetBundleResource(EventArgs args)
        {
            return null;
            //return assetManager.GetBundleResource(args);
        }
        #endregion

        #region Resource
        public object GetResource(string getDataToken, string resourceName)
        {
            return null;
            //return GetResource(new EventArgs(getDataToken, resourceName));
        }

        public object GetResource(string getDataToken, string performer, string resourceName)
        {
            return null;
            //return GetResource(new EventArgs(getDataToken, resourceName, performer));
        }

        object GetResource(EventArgs args)
        {
            return null;
            //return assetManager.GetResource(args);
        }
        #endregion
        #endregion

        #region Script
        public RecursiveScopeState<Dictionary<string, object>> CreateInstenceState { get; set; } = new RecursiveScopeState<Dictionary<string, object>>(new Dictionary<string, object>());

        #region NewObject
        public T GetNewObject<T>(string scriptToken = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            object tmp = GetNewObject(typeof(T).FullName, scriptToken, performer, parameters);
            if (tmp == null)
            {
                return default;
            }
            else
            {
                return (T)tmp;
            }
        }

        public object GetNewObject(Type scriptType, string scriptToken = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            return GetNewObject(scriptType.FullName, scriptToken, performer, parameters);
        }

        public object GetNewObject(string scriptType, string scriptToken, string performer = "", Dictionary<string, object> parameters = null)
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
                    tmpObject = AppConfig.Instence.GetMapping(scriptType, scriptToken).CreateInstence();
                }
                if (tmpObject == null)
                {
                    //tmpObject = resourcesManagers[ScriptManager].GetData(new InstenceArgs(scriptType, scriptToken, performer));
                }
                if (tmpObject == null)
                {
                    tmpObject = scriptType.CreateInstence();
                }


            }
            return tmpObject;
        }
        #endregion

        #region Object
        public T GetSingleton<T>()
        {
            return GetObject<T>();
        }

        public object GetSingleton(Type scriptType)
        {
            return GetObject(scriptType);
        }

        public T GetObject<T>(string scriptToken = "", string objectToken = "", string performer = RootLevel)
        {
            return (T)GetObject(typeof(T).FullName, scriptToken, objectToken, performer);
        }

        public object GetObject(Type scriptType, string scriptToken = "", string objectToken = "", string performer = RootLevel)
        {
            return GetObject(scriptType.FullName, scriptToken, objectToken, performer);
        }

        public object GetObject(string scriptType, string scriptToken = "", string objectToken = "", string performer = RootLevel)
        {
            //return resourcesManagers[ScriptManager].GetData(new ObjectArgs(scriptType, scriptToken, objectToken, performer));
            return null;
        }
        #endregion

        #region ObjectPool
        public object GetReusableObject()
        {
            return null;
        }
        #endregion

        #endregion

        #region GameObject
        #region NewGameObject
        public GameObject GetNewGameObject(string name, string performer = "")
        {
            return null;
            //return gameObjectManager.GetNewGameObject(name);
        }
        #endregion

        #region GameObject
        public GameObject GetGameObject(string name)
        {
            return null;
            //return gameObjectManager.GetGameObject(name);
        }
        #endregion 

        #region GameObjectPool
        public GameObject GetReusableGameObject(string name, string performer = "")
        {
            //return resourcesManagers[GameObjectManager].GetData(new ReusableGameObjectArgs(name, performer)) as GameObject;
            return null;
        }
        #endregion
        #endregion
        #endregion 
        #endregion

        #region Old
        //IDataNode assetManager;
        //IDataNode scriptManager;
        //IDataNode gameObjectManager;
        #endregion
    }
}