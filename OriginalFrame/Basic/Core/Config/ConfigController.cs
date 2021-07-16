using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public class ConfigController : StackRoutingController
    {
        protected override object CreateInstence()
        {
            return FrameManager.CreateInstence<IConfigProvider>();
        }

        public string GetMapping(string scriptType, string scriptId)
        {
            return "";
        }
    }
}