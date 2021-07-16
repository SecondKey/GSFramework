using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GSFramework.AppConst;

namespace GSFramework
{
    [Initialization_RoutingNode_Stack(RoutingBlock_Resources)]
    public class ResourcesLayer
    {
        [RoutingIdentifyProperty]
        public string ResourcesLayerID { get; set; }

        static Dictionary<string, IResourcesProvider> resourcesManagers = new Dictionary<string, IResourcesProvider>();

        ListState<string> loadResourcesState;

        [Inject(ParametersGetMode = Injection_Additional + "," + Injection_Additional)]
        public void LoadResources(string layer, Action<string> LoadOverAction)
        {
            ResourcesLayerID = layer;
            Debug.Log(ResourcesLayerID);
            loadResourcesState = new ListState<string>("");
            foreach (string s in resourcesManagers.Keys)
            {
                IResourcesProvider manager = FrameManager.GetMapping(typeof(IResourcesProvider).FullName, s).CreateInstence() as IResourcesProvider;
                //resourcesManagers[s].AddNode(manager);
            }
            loadResourcesState.TmpReductionStateEvent = LoadOverAction;
        }

        [InitEndFunction]
        public void LoadLayerEnd()
        {
            loadResourcesState.ExciteState(ResourcesLayerID);
        }
    }
}