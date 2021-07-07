using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class GameObjectPool : IGameObjectPool
    {
        Dictionary<string, List<GameObject>> ActiveObject = new Dictionary<string, List<GameObject>>();
        Dictionary<string, List<GameObject>> IdleObject = new Dictionary<string, List<GameObject>>();

        public object GetIdleObject(IRoutingEventArgs args)
        {
            //ReusableGameObjectArgs tmpArgs = args as ReusableGameObjectArgs;
            //GameObject tmpObject;
            //if (IdleObject.ContainsKey(tmpArgs.GameObjectName))
            //{
            //    tmpObject = IdleObject[tmpArgs.GameObjectName][0];
            //    IdleObject[tmpArgs.GameObjectName].RemoveAt(0);
            //}
            //else
            //{
            //    tmpObject = BasicManager.Instence.GetNewGameObject(tmpArgs.GameObjectName, tmpArgs.Performer as string);
            //}
            //if (ActiveObject.ContainsKey(tmpArgs.GameObjectName))
            //{
            //    ActiveObject[tmpArgs.GameObjectName].Add(tmpObject);
            //}
            //else
            //{
            //    ActiveObject.Add(tmpArgs.GameObjectName, new List<GameObject>() { tmpObject });
            //}
            //return tmpObject;
            return null;
        }

        public void Initialization() { }
    }
}