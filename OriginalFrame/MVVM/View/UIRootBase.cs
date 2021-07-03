using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class UIRootBase : UILogicNodeBase
    {
        public override IUIViewModel DataContext { get; set; }

        public Dictionary<string, EventHandler> RootEventHandlers { get; set; }

        public virtual void HandleRootEvent(EventArgs args) { } 
    }
}