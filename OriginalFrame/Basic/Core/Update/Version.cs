using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

namespace GSFramework
{
    struct FileUpdateInfo
    {

    }

    public class Version
    {
        public int MajorNumber;
        public int MinorNumber;
        public int RevisionNumber;

        VersionType VersionType;
        public DateTime ReleaseTime;

        public Version(XElement versionElement)
        {
            var versionNumber = versionElement.Element("Version").Value.Split('.');
            MajorNumber = int.Parse(versionNumber[0]);
            MinorNumber = int.Parse(versionNumber[1]);
            RevisionNumber = int.Parse(versionNumber[2]);

            var versionTime = versionElement.Element("Version").Value.Split('.');
            ReleaseTime = new DateTime(
                int.Parse(versionTime[0]),
                int.Parse(versionTime[1]),
                int.Parse(versionTime[2]));

            VersionType = (VersionType)Enum.Parse(typeof(VersionType), versionElement.Element("VersionType").Value, true);
        }

        //TODO：游戏版本更新
        public void Update()
        {

        }
        //TODO：游戏版本还原
        public void Reduction()
        {

        }
    }
}