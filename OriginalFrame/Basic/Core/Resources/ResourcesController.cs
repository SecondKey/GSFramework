using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ResourcesController : StackRoutingController
    {
        protected override object CreateNode()
        {
            return FrameManager.CreateInstence<IResourcesProvider>();
        }
    }
}
