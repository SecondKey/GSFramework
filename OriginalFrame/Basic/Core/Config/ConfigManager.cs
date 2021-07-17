using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 配置表管理器
    /// 加载以及存储配置信息
    /// </summary>
    public class ConfigManager
    {
        #region 单例
        private static ConfigManager instence;
        private ConfigManager() { FrameManager.RegistInstenceBeforInit(this, "Config", typeof(IRoutingController)); }
        public static ConfigManager Instence { get { if (instence == null) instence = new ConfigManager(); return instence; } }
        #endregion

        XElement mainConfig;
        Dictionary<string, XElement> configFiles;

        /// <summary>
        /// 加载程序配置表
        /// </summary>
        public void LoadConfig()
        {
            mainConfig = XDocument.Load(AppConst.Path["MainConfig"]).Root;

            configFiles = new Dictionary<string, XElement>();

            foreach (XElement path in mainConfig.Elements("Path").Elements())
            {
                AppConst.Path.Add(path.Name.ToString(), AppConst.DataPath + path.Value);
            }
            foreach (XElement configFile in mainConfig.Element("ConfigFile").Elements())
            {
                configFiles.Add(configFile.Name.ToString(), XDocument.Load($"{AppConst.Path[AppConst.Path_Config]}/{configFile.Value}.xml").Root);
            }
        }

        #region Mapping
        /// <summary>
        /// IOC映射表
        /// </summary>
        Dictionary<string, Dictionary<string, Type>> basicMapping;

        /// <summary>
        /// 加载IOC映射表
        /// </summary>
        void LoadIOCMappingsConfig()
        {
            basicMapping = new Dictionary<string, Dictionary<string, Type>>();
            foreach (XElement scriptType in configFiles[AppConst.Config_IOCMapping].Elements())
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

        /// <summary>
        /// 获取在Config中的IOC映射
        /// </summary>
        /// <param name="basicType">基础类型</param>
        /// <param name="id">具体实现的ID</param>
        /// <returns>目标类型</returns>
        public Type GetMapping(string basicType, string id)
        {
            if (basicMapping == null)
                LoadIOCMappingsConfig();

            if (!basicMapping.ContainsKey(basicType))
            {
                DevelopmentModeLog.BasicLogError($"在IOC映射表中没有找到指定的基础类型{basicType}");
                return null;
            }
            else if (!basicMapping[basicType].ContainsKey(id))
            {
                DevelopmentModeLog.BasicLogError($"在IOC映射表中，基础类型{basicType}没有注册{id}对应的实例");
                return null;
            }

            return basicMapping[basicType][id];
        }
        #endregion 
    }
}