using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace GSFramework
{
    /// <summary>
    /// 文件更新管理器
    /// </summary>
    public class VersionManager
    {
        List<Version> versionList;

        public void PerformUpdate()
        {
            foreach (DirectoryInfo NextFolder in new DirectoryInfo(AppConst.Path[AppConst.Path_Update]).GetDirectories())
            {

            }
        }

        void BuildVersionTree()
        {

        }
    }
}