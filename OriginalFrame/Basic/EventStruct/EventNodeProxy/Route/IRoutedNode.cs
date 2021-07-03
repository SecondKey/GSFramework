using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IRoutedNodeProxy : ITreeNodeProxy
    {
        IRoutedNodeProxy Parent { get; set; }
        IRoutedNodeProxy FindChild(object identify);
        IRoutedNodeProxy FindParent(object identify);
        IRoutedNodeProxy FindParent(int relativePosition);

        Dictionary<string, EventHandler> RoutedHandlers { get; set; }
        void HandleRoutedEvent(RoutedEventArgs args);
    }
}