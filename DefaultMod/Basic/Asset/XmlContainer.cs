using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using System.Linq;
using static GSFramework.Dev.DevelopmentModeLog;


namespace GSFramework.Default
{
    public class XmlContainer : IDataContainer
    {
        #region XmlConst
        public const string GetValue = "GetValue";
        public const string GetAllValue = "GetAllValue";
        #endregion

        WaitState<string> waitForLoadXmlState = new WaitState<string>("", true);
        Dictionary<string, XDocument> XmlDocuments = new Dictionary<string, XDocument>();

        IState<string> baseState;


        [DefaultConstructor(ParametersGetMode = AppConst.Injection_Additional)]
        public XmlContainer(string identify)
        {
            waitForLoadXmlState.MainRestore += () => { LogParameter(identify + " Xml Load Already"); baseState.RestoreState(); };
        }

        #region Load
        public void Load(string level, IState<string> state)
        {
            baseState = state;

            Dictionary<string, string> xmls = new Dictionary<string, string>();
            switch (level)
            {
                case AppConst.ResourcesLevel_RootLevel:
                    foreach (FileInfo file in new DirectoryInfo(AppConst.Path[level]).GetFiles("*.xml"))
                    {
                        xmls.Add(file.Name.Replace(".xml", ""), file.FullName);
                    }
                    break;
                case AppConst.ResourcesLevel_GeneralLevel:
                    foreach (var kv in FrameManager.GetData(GetAllValue, AppConst.ResourcesLevel_RootLevel, "AppData", "Load", "Data") as Dictionary<string, string>)
                    {
                        xmls.Add(kv.Key, AppConst.Path[AppConst.ResourcesLevel_GeneralLevel] + kv.Value);
                    }
                    break;
            }

            foreach (var kv in xmls)
            {
                waitForLoadXmlState.ExciteState(kv.Value);
                RunTimeTools.Instence.StartCoroutine(CoroutineLoadXml(kv.Key, kv.Value));
            }

            waitForLoadXmlState.AddAlready();
        }

        /// <summary>
        /// 加载xml文件
        /// </summary>
        /// <param name="xmlName">xml文件名</param>
        /// <param name="xmlPath">xml在游戏目录下的完整路径</param>
        /// <returns></returns>
        IEnumerator CoroutineLoadXml(string xmlName, string xmlPath)
        {
            LogParameter("加载XML：", xmlName, xmlPath);
            using (UnityWebRequest www = new UnityWebRequest(xmlPath))
            {
                www.downloadHandler = new DownloadHandlerBuffer();
                yield return www.SendWebRequest();
                string text = RemoveUtf8ByteOrderMark(www.downloadHandler.text);
                XDocument xDoc = XDocument.Parse(text);
                XmlDocuments.Add(xmlName, xDoc);
                waitForLoadXmlState.RestoreState(xmlPath);
            }
        }
        #endregion

        #region Getter
        [EventBinding(GetValue)]
        public object GetValueAction(IRoutingEventArgs args)
        {
            //string[] parameters = (args as EventArgs<string[]>).Parameter;
            //string[] parameters = (args as EventArgs<string[]>).Parameter;
            //if (!XmlDocuments.ContainsKey(parameters[0]))
            //{
            //    return null;
            //}
            //XElement e = XmlDocuments[parameters[0]].Root;
            //for (int i = 1; i < parameters.Length; i++)
            //{
            //    e = e.Element(parameters[i]);
            //}
            //return e.Value;
            return null;
        }

        [EventBinding(GetAllValue)]
        public object GetAllValueAction(IRoutingEventArgs args)
        {
            //string[] parameters = (args as EventArgs<string[]>).Parameter;
            //if (!XmlDocuments.ContainsKey(parameters[0]))
            //{
            //    return null;
            //}
            //Dictionary<string, string> tmpDic = new Dictionary<string, string>();
            //XElement e = XmlDocuments[parameters[0]].Root;
            //for (int i = 1; i < parameters.Length; i++)
            //{
            //    e = e.Element(parameters[i]);
            //}
            //foreach (XElement element in e.Elements())
            //{
            //    tmpDic.Add(element.Name.ToString(), element.Value);
            //}
            //return tmpDic;
            return null;
        }
        #endregion 

        #region Tools
        /// <summary>
        /// 移出utf8的头文件
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public string RemoveUtf8ByteOrderMark(string xml)
        {
            string byteOrderMarkUtf8 = Encoding.UTF8.GetString(Encoding.UTF8.GetPreamble());
            if (xml.StartsWith(byteOrderMarkUtf8))
            {
                xml = xml.Remove(0, byteOrderMarkUtf8.Length);
            }
            return xml;
        }

        /// <summary>
        /// 输出每一次访问传入的参数
        /// </summary>
        /// <param name="nam"></param>
        /// <param name="parameter"></param>
        public void LogParameter(string nam, params string[] parameter)
        {
            for (int i = 0; i < parameter.Length; i++)
            {
                nam = nam + " " + parameter[i];
            }
            BasicLog("Xml:" + nam);
        }

        #endregion

    }
}