using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IGameData
    {
        object GetData(string dataPath);
        bool CheckConditions(string Conditions);
        bool CheckConditions(params string[] parametesr);
    }
}