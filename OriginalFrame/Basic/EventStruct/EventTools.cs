using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class EventTools
    {
        public static void HandleEvent(this object eventNode, string eventName, params object[] parameters)
        {

        }

        public static object GetData(this object eventNode, string eventName, params object[] parameters)
        {
            return null;
        }
    }
}