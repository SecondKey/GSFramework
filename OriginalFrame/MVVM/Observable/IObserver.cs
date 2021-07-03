using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public interface IObserver
    {
        void PropertyChanged(string propertyName, object value);
    }
}