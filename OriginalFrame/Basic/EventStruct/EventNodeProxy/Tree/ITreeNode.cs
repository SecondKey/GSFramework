using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ITreeNodeProxy
    {
        int Deep { get; }
        //ITreeRoot Root { get; }
        Dictionary<object, ITreeNodeProxy> NextCollection { get; }

        void RemoveDeep(int deep);
    }
}