using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    [Injectable_Initialization]
    public abstract class ResourcesManagerBase : IResourcesManager
    {
        [Inject(ParametersGetMode = AppConst.Injection_Additional)]
        public string Identify { get; set; }
        public Dictionary<string, EventHandler> Handlers { get; set; }
        public Dictionary<string, DataProvider> Getters { get; set; }


        #region IInitializableObject Members
        public virtual void Initialization() { }
        #endregion 


    }
}