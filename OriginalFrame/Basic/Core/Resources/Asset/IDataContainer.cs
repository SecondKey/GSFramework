﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDataContainer
    {
        void Load(string level, IState<string> state);

        Dictionary<string, DataProvider> Getters { get; set; }
        object GetData(IRoutingEventArgs args);
    }
}