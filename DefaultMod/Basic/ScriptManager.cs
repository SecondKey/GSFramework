using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    [Initialization_Injectable]
    public class ScriptManager : ResourcesManagerBase
    {
        [Inject]
        IIOCContainer iocContainer { get; set; }
        [Inject]
        IObjectContainer objectContainer { get; set; }
        [Inject]
        IObjectPool objectPool { get; set; }

        #region Handler
        [EventBinding]
        void Load(IRoutingEventArgs args)
        {
            //EventArgs tmpArgs = args as EventArgs;
            DevelopmentModeLog.BasicLog($"Start Load {Identify} Script");
            //tmpArgs.Parameter.RestoreState();
        }
        #endregion 

        #region Getter
        #region NewInstence
        [EventBinding("GetNewObject")]
        object GetNewObject(IRoutingEventArgs args)
        {
            InstenceArgs tmpArgs = args as InstenceArgs;
            return iocContainer.CreateInstence(tmpArgs.ScriptType, tmpArgs.ScriptToken);
        }
        #endregion

        #region Object
        [EventBinding("GetObject")]
        public object GetObject(IRoutingEventArgs args)
        {
            ObjectArgs tmpArgs = args as ObjectArgs;
            return objectContainer.GetObject(tmpArgs.ScriptType, tmpArgs.ScriptToken, tmpArgs.ObjectToken);
        }
        #endregion

        [EventBinding("GetIdleObject")]
        public object GetIdleObject(IRoutingEventArgs args)
        {
            ObjectArgs tmpArgs = args as ObjectArgs;
            return objectPool.GetIdleObject();
        }
        #endregion

    }
}