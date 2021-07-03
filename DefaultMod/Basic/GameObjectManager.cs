using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    [Injectable_Initialization]
    public class GameObjectManager : ResourcesManagerBase
    {
        [Inject]
        IAssembler assembler { get; set; }
        [Inject]
        IGameObjectPool gameObjectPool { get; set; }

        #region Handler
        [EventBinding("Load")]
        void Load(EventArgs args)
        {
            EventArgs<IState<string>> tmpArgs = args as EventArgs<IState<string>>;
            DevelopmentModeLog.BasicLog($"Start Load {Identify} GameObject");
            tmpArgs.Parameter.RestoreState();
        }
        #endregion

        #region Getter
        public GameObject GetNewGameObject(string objectName)
        {
            return null;
        }

        public GameObject GetGameObject(string objectName)
        {
            return null;
        }
        #endregion
    }
}