using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC
{
    public interface IMVCFacade
    {
        void ExecuteCommand(string commandName, params object[] parameters);
        IModel GetModel(string modelName);
        IView GetView(string viewName);
    }
}