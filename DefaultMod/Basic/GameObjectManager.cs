using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    [Initialization_Injectable]
    public class GameObjectManager : ResourcesManagerBase
    {
        [Inject]
        IAssembler assembler { get; set; }
        [Inject]
        IGameObjectPool gameObjectPool { get; set; }

        #region Handler
        [EventBinding("Load")]
        void Load(IRoutingEventArgs args)
        {
            DevelopmentModeLog.BasicLog($"Start Load {Identify} GameObject");
            (args.Parameters[0] as IState<string>).RestoreState();
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