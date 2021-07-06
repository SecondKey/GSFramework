using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public static class RoutingSystemCenter
    {
        public static void HandleEvent(this object routingNode, string eventName, params object[] parameters)
        {

        }

        public static void HandleEvent(this object routingNode, EventArgs args)
        {

        }

        public static void HandleEvent(string routingName, EventArgs args)
        {

        }

        public static object GetData(this object routingNode, string eventName, params object[] parameters)
        {
            return null;
        }

        public static object GetData(this object routingNode, EventArgs args)
        {
            return null;
        }

        public static object GetData(string routingNode, EventArgs args)
        {
            return null;
        }
    }
}