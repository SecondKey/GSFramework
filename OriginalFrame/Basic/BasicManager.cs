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
        /// <param name="initalizationOverAction">初始化结束后s的回调</param>
        public void GameInitalization(Action initalizationOverAction)
        {
            onInitalizationOver = initalizationOverAction;
            foreach (string m in AppConfig.Instence.ManagerList)
            {
                IResourcesManager manager = GetNewObject<IResourcesManager>(m, "", new Dictionary<string, object>() { { "Identify", RootLevel } });
                //AppConfig.Instence.GetMapping(typeof(IResourcesManager).FullName, m).CreateInstence() as IResourcesManager;
                resourcesManagers.Add(m, manager);
                loadResourcesState.AddState((s) => { manager.HandleEvent(new EventArgs<IState<string>>("Load", loadResourcesState, s)); });
            }
            loadResourcesState.TmpReductionStateEvent += (s) => { StartUpdate(); };
            loadResourcesState.ExciteState(RootLevel);
        }
        /// <summary>
        /// 开始更新
        /// </summary>
        void StartUpdate()
        {
            IUpdateManager updateManager = GetNewObject<IUpdateManager>();
            UpdateState.TmpRestoreStateEvent += (b) => { InitalizationOver(); };
            UpdateState.ExciteState(true);
            updateManager.Update(UpdateState);
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
        /// <summary>
        /// 包含一系列资源加载方法的链式状态
        /// 其中的方法将顺序加载，以保证其中的依赖性
        /// </summary>
        internal ListState<string> loadResourcesState { get; } = new ListState<string>("");
        /// <summary>
        /// 包含一系列的资源管理器
        /// 这些资源管理器被设计成链表的结构，以方便动态加载卸载。
        /// 这里包含所有类型的管理器的头节点。
        /// </summary>
        Dictionary<string, IResourcesManager> resourcesManagers = new Dictionary<string, IResourcesManager>();
        /// <summary>
        /// 加载一个资源层级，
        /// 在加载时不会自动加载前置层级，因此在调用时需要保证其前置层级已经加载
        /// </summary>
        /// <param name="level">需要加载的资源层级</param>
        /// <param name="over">加载完成的回调函数</param>
        public void LoadResoureces(string level, Action<string> over)
        {
            foreach (string s in resourcesManagers.Keys)
            {
                IResourcesManager manager = AppConfig.Instence.GetMapping(typeof(IResourcesManager).FullName, s).CreateInstence() as IResourcesManager;
                resourcesManagers[s].AddNode(manager);
            }
            loadResourcesState.TmpReductionStateEvent = over;
            loadResourcesState.ExciteState(level);
        }
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
            return resourcesManagers[AssetManager].GetData(args);
        }
        #endregion

        #region Bundle
        public object GetBundleResource(string getDataToken, params string[] parameters)
        {
            return GetBundleResource(new EventArgs<string[]>(getDataToken, parameters));
        }

        public object GetBundleResource(string getDataToken, string performer, params string[] parameters)
        {
            return GetBundleResource(new EventArgs<string[]>(getDataToken, parameters, performer));
        }

        object GetBundleResource(EventArgs<string[]> args)
        {
            return null;
            //return assetManager.GetBundleResource(args);
        }
        #endregion

        #region Resource
        public object GetResource(string getDataToken, string resourceName)
        {
            return GetResource(new EventArgs<string>(getDataToken, resourceName));
        }

        public object GetResource(string getDataToken, string performer, string resourceName)
        {
            return GetResource(new EventArgs<string>(getDataToken, resourceName, performer));
        }

        object GetResource(EventArgs<string> args)
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
                    tmpObject = resourcesManagers[ScriptManager].GetData(new InstenceArgs(scriptType, scriptToken, performer));
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
            return resourcesManagers[ScriptManager].GetData(new ObjectArgs(scriptType, scriptToken, objectToken, performer));
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
            return resourcesManagers[GameObjectManager].GetData(new ReusableGameObjectArgs(name, performer)) as GameObject;
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