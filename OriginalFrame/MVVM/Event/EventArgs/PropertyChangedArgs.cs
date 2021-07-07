using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class PropertyChangedEventArgs : TopToBottomEventArgs
    {
        public string PropertyName { get; }
        public object Value { get; }
        public PropertyChangedEventArgs(string propertyName, object value) : base("PropertyChanged", null)
        {
            PropertyName = propertyName;
            Value = value;
        }
    }
}