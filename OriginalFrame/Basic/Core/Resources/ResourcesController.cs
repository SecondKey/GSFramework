using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ResourcesController : StackRoutingController
    {
        protected override object CreateInstence()
        {
            return FrameManager.CreateInstence<IResourcesProvider>();
        }
    }
}
