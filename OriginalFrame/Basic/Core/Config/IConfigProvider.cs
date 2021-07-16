using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IConfigProvider
    {
        [Inject(ParametersGetMode = AppConst.Injection_Additional)]
        void LoadConfig(string configLayer);
    }
}
