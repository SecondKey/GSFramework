using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface ITreeRoutingNodeProxy : IRoutingNodeProxy
    {
        int Deep { get; }
        //ITreeRoot Root { get; }
        Dictionary<object, ITreeRoutingNodeProxy> NextCollection { get; }

        void RemoveDeep(int deep);
    }
}