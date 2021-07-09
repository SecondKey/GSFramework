using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class AssetManager : IResourcesProvider
    {
        [Inject]
        IDataContainer dataContainer { get; set; }
        [Inject]
        IBundleContainer bundleContainer { get; set; }
        [Inject]
        ILocalResourcesContainer resourceContainer { get; set; }

        [InitMiddleFunction]
        public void Initialization()
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
        void Load(IRoutingEventArgs args)
        {
            ////EventArgs tmpArgs = args as EventArgs;
            //DevelopmentModeLog.BasicLog($"Start Load {Identify} Asset");
            ////baseState = tmpArgs.Parameter;
            //LoadLevelState.ExciteState(Identify);
        }
        #endregion

        #region Getter
        #region Data
        [EventBinding("GetData")]
        public object GetGameData(IRoutingEventArgs args)
        {
            //GetDataEventArgs tmpArgs = args as GetDataEventArgs;
            return null;
            //return dataContainer.GetData(new EventArgs(tmpArgs.GetMode, tmpArgs.Parameter));
        }
        #endregion

        #region Bundle
        public object GetBundleResource(IRoutingEventArgs args)
        {
            return null;
            //return bundleContainer.GetData(args);
        }
        #endregion

        #region Resource
        public object GetResource(IRoutingEventArgs args)
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