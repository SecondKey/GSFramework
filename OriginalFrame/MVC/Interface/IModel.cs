using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GSFramework.MVC.MSGFrame;

namespace GSFramework.MVC
{
    public interface IModel : INotifiedObject
    {
        string ModelName { get; }
    }
}