using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.Default
{
    public class ObjectContainer : IObjectContainer
    {
        private Dictionary<string, Dictionary<string, object>> objectDictionary = new Dictionary<string, Dictionary<string, object>>();

        public object GetObject(string scriptType, string scripToken, string singletonToken)
        {
            object tmp = null;
            if (objectDictionary.ContainsKey(scriptType))
            {
                if (objectDictionary[scriptType].ContainsKey(singletonToken))
                {
                    tmp = objectDictionary[scriptType][singletonToken];
                }
                else
                {
                    tmp = BasicManager.Instence.GetNewObject(scriptType, scripToken);
                    objectDictionary[scriptType].Add(singletonToken, tmp);
                }
            }
            else
            {
                tmp = BasicManager.Instence.GetNewObject(scriptType, scripToken);
                objectDictionary.Add(scriptType, new Dictionary<string, object>() { { singletonToken, tmp } });
            }
            return tmp;
        }
    }
}