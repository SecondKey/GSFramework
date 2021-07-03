using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    [Injectable_Initialization]
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
        void Load(EventArgs args)
        {
            EventArgs<IState<string>> tmpArgs = args as EventArgs<IState<string>>;
            DevelopmentModeLog.BasicLog($"Start Load {Identify} Script");
            tmpArgs.Parameter.RestoreState();
        }
        #endregion 

        #region Getter
        #region NewInstence
        [EventBinding("GetNewObject")]
        object GetNewObject(EventArgs args)
        {
            InstenceArgs tmpArgs = args as InstenceArgs;
            return iocContainer.CreateInstence(tmpArgs.ScriptType, tmpArgs.ScriptToken);
        }
        #endregion

        #region Object
        [EventBinding("GetObject")]
        public object GetObject(EventArgs args)
        {
            ObjectArgs tmpArgs = args as ObjectArgs;
            return objectContainer.GetObject(tmpArgs.ScriptType, tmpArgs.ScriptToken, tmpArgs.ObjectToken);
        }
        #endregion

        [EventBinding("GetIdleObject")]
        public object GetIdleObject(EventArgs args)
        {
            ObjectArgs tmpArgs = args as ObjectArgs;
            return objectPool.GetIdleObject();
        }
        #endregion

    }
}