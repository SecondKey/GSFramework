﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework
{
    public interface IAssembler : IInitializableObject
    {
        object GetGameObject(IRoutingEventArgs args);
    }
}