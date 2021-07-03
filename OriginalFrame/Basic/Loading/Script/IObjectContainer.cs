using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IObjectContainer
    {
        object GetObject(string scriptType, string scripToken, string objectToken);
    }
}