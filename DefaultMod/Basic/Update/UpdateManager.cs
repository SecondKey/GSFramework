using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class UpdateManager : IUpdateManager
    {
        public void Update(IState<bool> updateState)
        {
            updateState.RestoreState();
        }
    }
}