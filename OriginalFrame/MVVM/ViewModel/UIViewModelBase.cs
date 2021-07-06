using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace GSFramework.MVVM
{
    public class UIViewModelBase : ObservableObject, IUIViewModel
    {
        //Dictionary<string, UIBinderProxy> DataBindingList = new Dictionary<string, UIBinderProxy>();

        IObservableObject model;
        public IObservableObject Model
        {
            get { return model; }
            set
            {
                if (model != value)
                {
                    if (model != null)
                    {
                        BasicManager.Instence.GetSingleton<IObservableManager>().UnbindData(Model, this);
                    }
                    model = value;
                    BasicManager.Instence.GetSingleton<IObservableManager>().BindingData(Model, this);
                }
            }
        }

        public void BindingComponent(string objectName, IUIBinder binder)
        {
            //UIBinderProxy proxy = BasicManager.Instence.GetNewObject<UIBinderProxy>("", "", new Dictionary<string, object>() { { "Identify", binder } });

            //if (!DataBindingList.ContainsKey(objectName))
            //{
            //    DataBindingList.Add(objectName, proxy);
            //}
            //else
            //{
            //    DataBindingList[objectName].AddNode(proxy);
            //}
        }

        public override void ExecuteCommand(string command, object parameter)
        {
            if (Commands.ContainsKey(command))
            {
                Commands[command].Execute(parameter);
            }
            else
            {
                model.ExecuteCommand(command, parameter);
            }
        }

        public override object GetData(string propertyName)
        {
            object tmpObject = base.GetData(propertyName);
            if (tmpObject == null)
            {
                tmpObject = model.GetData(propertyName);
            }
            return tmpObject;
        }

        public void PropertyChanged(string propertyName, object value)
        {
            //if (DataBindingList.ContainsKey(propertyName))
            //{
            //    DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, value));
            //}
        }

        public override void RaisePropertyChanged([CallerMemberName] string propertyName = "")
        {
            //if (DataBindingList.ContainsKey(propertyName))
            //{
            //    DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, GetType().GetProperty(propertyName).GetValue(this)));
            //}
        }

        public override void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            //string propertyName = (propertyExpression.Body as MemberExpression).Member.Name;
            //if (DataBindingList.ContainsKey(propertyName))
            //{
            //    DataBindingList[propertyName].HandleEvent(new PropertyChangedEventArgs(propertyName, propertyExpression.Compile().Invoke()));
            //}
        }
    }
}