using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using UnityEngine;

namespace GSFramework
{
    public class AppConfig
    {
        XDocument configFile;
        Dictionary<string, Action<XElement>> LoadConfigAction;
        Dictionary<string, XDocument> configFiles;

        private static AppConfig instence;
        public static AppConfig Instence
        {
            get
            {
                if (instence == null)
                {
                    instence = new AppConfig();
                }
                return instence;
            }
        }

        public AppConfig()
        {
            configFile = XDocument.Load(AppConst.AssetPath["AppConfig"]);
            foreach (XElement path in configFile.Root.Element("Path").Elements())
            {
                AppConst.AssetPath.Add(path.Name.ToString(), AppConst.DataPath + path.Value + "/");
            }

            LoadConfigAction = new Dictionary<string, Action<XElement>>()
            {
                { "IOCMapping",LoadIOCMappingsConfig},
                { "Resources",LoadResourcesConfig},
                { "Routing",LoadRoutingConfig}
            };
            foreach (var kv in LoadConfigAction)
            {
                kv.Value.Invoke(XDocument.Load(AppConst.AssetPath["Config"] + kv.Key + ".xml").Root);
            }
        }



        #region Mapping
        Dictionary<string, Dictionary<string, Type>> basicMapping = new Dictionary<string, Dictionary<string, Type>>();

        void LoadIOCMappingsConfig(XElement element)
        {
            foreach (XElement scriptType in element.Elements())
            {
                basicMapping.Add(scriptType.Name.ToString(), new Dictionary<string, Type>());
                if (!string.IsNullOrEmpty(scriptType.Value))
                {
                    basicMapping[scriptType.Name.ToString()].Add("", Type.GetType(scriptType.Value));
                }
                foreach (XElement scriptToken in scriptType.Elements())
                {
                    basicMapping[scriptType.Name.ToString()].Add(scriptToken.Name.ToString(), Type.GetType(scriptToken.Value));
                }
            }
        }

        public Type GetMapping(string baiscType, string token)
        {
            if (!basicMapping.ContainsKey(baiscType) || !basicMapping[baiscType].ContainsKey(token))
            {
                return null;
            }

            return basicMapping[baiscType][token];
        }
        #endregion

        #region ResourcesManager
        List<string> managerList = new List<string>();
        public List<string> ManagerList { get { return managerList; } }
        public void LoadResourcesConfig(XElement element)
        {
            foreach (XElement e in element.Elements())
            {
                managerList.Add(e.Name.ToString());
            }
        }
        #endregion 

        #region Routing

        public void LoadRoutingConfig(XElement element)
        {
            //foreach (XElement e in element.Elements())
            //{
            //    managerList.Add(e.Name.ToString());
            //}
        }
        #endregion 
    }
}
