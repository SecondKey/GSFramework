using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class ObjectPool : IObjectPool
    {
        Dictionary<string, List<object>> ActiveObject = new Dictionary<string, List<object>>();
        Dictionary<string, List<object>> IdleObject = new Dictionary<string, List<object>>();

        public object GetActionObject()
        {
            throw new System.NotImplementedException();
        }

        public object GetIdleObject(EventArgs args)
        {
            ReusableObjectArgs tmpArgs = args as ReusableObjectArgs;
            object tmpObject;
            if (IdleObject.ContainsKey(tmpArgs.Group))
            {
                tmpObject = IdleObject[tmpArgs.Group][0];
            }
            else
            {
                tmpObject = BasicManager.Instence.GetNewObject(tmpArgs.ScriptType, tmpArgs.ScriptToken, tmpArgs.Performer as string);
            }
            if (ActiveObject.ContainsKey(tmpArgs.Group))
            {
                ActiveObject[tmpArgs.Group].Add(tmpObject);
            }
            else
            {
                ActiveObject.Add(tmpArgs.Group, new List<object>() { tmpObject });
            }
            return tmpObject;
        }

        public object GetIdleObject()
        {
            throw new System.NotImplementedException();
        }
    }
}