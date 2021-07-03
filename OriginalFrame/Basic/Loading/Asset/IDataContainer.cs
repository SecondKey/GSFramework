using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IDataContainer : IInitializableObject
    {
        void Load(string level, IState<string> state);

        Dictionary<string, EventGetter> Getters { get; set; }
        object GetData(EventArgs args);
    }
}