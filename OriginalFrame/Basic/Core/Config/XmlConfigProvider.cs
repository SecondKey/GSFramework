//using System;
//using System.Collections;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Xml.Linq;
//using UnityEngine;

//namespace GSFramework
//{
//    public class XmlConfigProvider : IConfigProvider
//    {
//        XDocument configFile;
//        Dictionary<string, Action<XElement>> LoadConfigAction;
//        Dictionary<string, XDocument> configFiles;

//        public void LoadConfig(string configLayer)
//        {
//            configFile = XDocument.Load(AppConst.AssetPath["AppConfig"]);
//            foreach (XElement path in configFile.Root.Element("Path").Elements())
//            {
//                AppConst.AssetPath.Add(path.Name.ToString(), AppConst.DataPath + path.Value + "/");
//            }

//            LoadConfigAction = new Dictionary<string, Action<XElement>>()
//            {
//                { "IOCMapping",LoadIOCMappingsConfig},
//                //{ "Resources",LoadResourcesConfig},
//                { "Routing",LoadRoutingConfig}
//            };
//            foreach (var kv in LoadConfigAction)
//            {
//                kv.Value.Invoke(XDocument.Load(AppConst.AssetPath["Config"] + kv.Key + ".xml").Root);
//            }
//        }



//        #region Mapping

//        #endregion

//        #region Routing

//        public void LoadRoutingConfig(XElement element)
//        {

//        }
//        #endregion 
//    }
//}
