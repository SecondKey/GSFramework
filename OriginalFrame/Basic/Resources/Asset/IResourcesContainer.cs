using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ILocalResourcesContainer
    {
        void Load(string level, IState<string> state);
    }
}