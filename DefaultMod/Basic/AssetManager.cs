using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class AssetManager : ResourcesManagerBase
    {
        [Inject]
        IDataContainer dataContainer { get; set; }
        [Inject]
        IBundleContainer bundleContainer { get; set; }
        [Inject]
        IResourceContainer resourceContainer { get; set; }

        public override void Initialization()
        {
            LoadLevelState.AddState((s) => { dataContainer.Load(s, LoadLevelState); });
            LoadLevelState.AddState((s) => { bundleContainer.Load(s, LoadLevelState); });
            LoadLevelState.AddState((s) => { resourceContainer.Load(s, LoadLevelState); }); LoadLevelState.TmpReductionStateEvent = (s) => { baseState.RestoreState(); };
            LoadLevelState.TmpReductionStateEvent = (s) => { baseState.RestoreState(); };
        }
        #region Handler
        IState<string> baseState;
        public ListState<string> LoadLevelState { get; set; } = new ListState<string>("");

        [EventBinding]
        void Load(EventArgs args)
        {
            EventArgs<IState<string>> tmpArgs = args as EventArgs<IState<string>>;
            DevelopmentModeLog.BasicLog($"Start Load {Identify} Asset");
            baseState = tmpArgs.Parameter;
            LoadLevelState.ExciteState(Identify);
        }
        #endregion

        #region Getter
        #region Data
        [EventBinding("GetData")]
        public object GetGameData(EventArgs args)
        {
            GetDataEventArgs tmpArgs = args as GetDataEventArgs;
            return dataContainer.GetData(new EventArgs<string[]>(tmpArgs.GetMode, tmpArgs.Parameter));
        }
        #endregion

        #region Bundle
        public object GetBundleResource(EventArgs<string[]> args)
        {
            return null;
            //return bundleContainer.GetData(args);
        }
        #endregion

        #region Resource
        public object GetResource(EventArgs<string> args)
        {
            return null;
            //return resourcesContainer.GetData(args);
        }
        #endregion

        #endregion

        #region Tools
        public bool CheckConditions(string conditions)
        {
            return CheckConditions(new List<string>(conditions.Split(',')));
        }

        public bool CheckConditions(List<string> conditions)
        {
            foreach (string condition in conditions)
            {
                if (!CheckCondition(condition))
                {
                    return false;
                }
            }
            return true;
        }

        public bool CheckCondition(string condition)
        {
            return true;
        }
        #endregion
    }
}