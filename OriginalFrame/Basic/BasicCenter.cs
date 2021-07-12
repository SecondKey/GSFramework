using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class BasicCenter
    {
        /// <summary>
        /// 游戏初始化
        /// </summary>
        public static void GameInit()
        {
            //AppConfig.Instence;
        }

        #region Resources
        static ResourcesController resourcesController;
        #region Instence

        #region CreateInstence
        public static RecursiveScopeState<Dictionary<string, object>> CreateInstenceState { get; set; } = new RecursiveScopeState<Dictionary<string, object>>(new Dictionary<string, object>());

        public static T CreateInstence<T>(string scriptToken = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            object tmp = CreateInstence(typeof(T).FullName, scriptToken, performer, parameters);
            if (tmp == null)
            {
                return default;
            }
            else
            {
                return (T)tmp;
            }
        }

        public static object CreateInstence(Type scriptType, string scriptToken = "", string performer = "", Dictionary<string, object> parameters = null)
        {
            return CreateInstence(scriptType.FullName, scriptToken, performer, parameters);
        }

        public static object CreateInstence(string scriptType, string scriptToken, string performer = "", Dictionary<string, object> parameters = null)
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
                    tmpObject = resourcesController.GetData(new InstenceArgs(scriptType, scriptToken, performer));
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
            return resourcesController.GetData(new TopToBottomEventArgs(AppConst.Resources_GetInstence, scriptType, scriptToken, objectToken));
        }


        #endregion
        #endregion

        #endregion

        #region Update
        public static void Update(IState<bool> updateState)
        {
            updateState.RestoreState();
        }
        #endregion 
    }
}