using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

namespace GSFramework.MVC
{
    public class MVCFacade : IMVCFacade
    {
        public void ExecuteCommand(string commandName, params object[] parameters)
        {
            ICommand command = FrameManager.CreateInstence<ICommand>(commandName);
            command.Execute(parameters);
        }

        public IModel GetModel(string modelName)
        {
            return FrameManager.GetInstence<IModel>(modelName);
        }

        public IView GetView(string viewName)
        {
            throw new NotImplementedException();
        }
    }
    //public class MVCFacade : IMVCFacade
    //{
    //    public MVCFacade(string key)
    //    {
    //        InitializeNotifier(key);
    //        m_instanceMap[key] = this;
    //        InitializeFacade();
    //    }

    //    public void RegisterProxy(IProxy proxy)
    //    {
    //        m_model.RegisterProxy(proxy);
    //    }
    //    public IProxy RetrieveProxy(string proxyName)
    //    {
    //        return m_model.RetrieveProxy(proxyName);
    //    }
    //    public IProxy RemoveProxy(string proxyName)
    //    {
    //        return m_model.RemoveProxy(proxyName);
    //    }
    //    public bool HasProxy(string proxyName)
    //    {
    //        return m_model.HasProxy(proxyName);
    //    }
    //    public void RegisterCommand(string notificationName, Type commandType)
    //    {
    //        // The controller is initialized in the constructor of the singleton, so this call should be thread safe.
    //        // This method is thread safe on the controller.
    //        m_controller.RegisterCommand(notificationName, commandType);
    //    }
    //    public void RegisterCommand(string notificationName, ICommand command)
    //    {
    //        m_controller.RegisterCommand(notificationName, command);
    //    }
    //    public object RemoveCommand(string notificationName)
    //    {
    //        return m_controller.RemoveCommand(notificationName);
    //    }
    //    public bool HasCommand(string notificationName)
    //    {
    //        return m_controller.HasCommand(notificationName);
    //    }
    //    public void RegisterMediator(IMediator mediator)
    //    {
    //        m_view.RegisterMediator(mediator);
    //    }
    //    public IMediator RetrieveMediator(string mediatorName)
    //    {
    //        return m_view.RetrieveMediator(mediatorName);
    //    }
    //    public IMediator RemoveMediator(string mediatorName)
    //    {
    //        return m_view.RemoveMediator(mediatorName);
    //    }
    //    public bool HasMediator(string mediatorName)
    //    {
    //        return m_view.HasMediator(mediatorName);
    //    }

    //    public void NotifyObservers(INotification notification)
    //    {
    //        m_view.NotifyObservers(notification);
    //    }

    //    public override void SendNotification(string notificationName)
    //    {
    //        NotifyObservers(new Notification(notificationName));
    //    }

    //    public override void SendNotification(string notificationName, object body)
    //    {
    //        NotifyObservers(new Notification(notificationName, body));
    //    }

    //    public override void SendNotification(string notificationName, object body, string type)
    //    {
    //        NotifyObservers(new Notification(notificationName, body, type));
    //    }

    //    public static IMVCFacade GetInstance(string key)
    //    {
    //        IMVCFacade result;
    //        if (m_instanceMap.TryGetValue(key, out result))
    //            return result;

    //        result = new MVCFacade(key);
    //        m_instanceMap[key] = result;
    //        return result;
    //    }

    //    public static bool HasCore(string key)
    //    {
    //        return m_instanceMap.ContainsKey(key);
    //    }
    //    public static IEnumerable<string> ListCore
    //    {
    //        get { return m_instanceMap.Keys; }
    //    }

    //    public static void RemoveCore(string key)
    //    {
    //        IMVCFacade facade;
    //        if (!m_instanceMap.TryGetValue(key, out facade))
    //            return;

    //        m_instanceMap.Remove(key);

    //        ModelManager.RemoveModel(key);
    //        ControllerManager.RemoveController(key);
    //        ViewManager.RemoveView(key);
    //    }

    //    public void Dispose()
    //    {
    //        m_view = null;
    //        m_model = null;
    //        m_controller = null;
    //        m_instanceMap.Remove(MultitonKey);
    //    }

    //    public static void BroadcastNotification(INotification notification)
    //    {
    //        foreach (var facade in m_instanceMap)
    //            facade.Value.NotifyObservers(notification);
    //    }

    //    public static void BroadcastNotification(string notificationName)
    //    {
    //        BroadcastNotification(new Notification(notificationName));
    //    }

    //    public static void BroadcastNotification(string notificationName, object body)
    //    {
    //        BroadcastNotification(new Notification(notificationName, body));
    //    }

    //    public static void BroadcastNotification(string notificationName, object body, string type)
    //    {
    //        BroadcastNotification(new Notification(notificationName, body, type));
    //    }

    //    static MVCFacade()
    //    {
    //        m_instanceMap = new ConcurrentDictionary<string, IMVCFacade>();
    //    }

    //    protected virtual void InitializeFacade()
    //    {
    //        InitializeModel();
    //        InitializeController();
    //        InitializeView();
    //    }

    //    protected virtual void InitializeController()
    //    {
    //        if (m_controller != null) return;
    //        m_controller = ControllerManager.GetInstance(MultitonKey);
    //    }

    //    protected virtual void InitializeModel()
    //    {
    //        if (m_model != null) return;
    //        m_model = ModelManager.GetInstance(MultitonKey);
    //    }

    //    protected virtual void InitializeView()
    //    {
    //        if (m_view != null) return;
    //        m_view = ViewManager.GetInstance(MultitonKey);
    //    }

    //    protected IControllerManager m_controller;

    //    protected IModelManager m_model;

    //    protected IViewManager m_view;

    //    protected static readonly IDictionary<string, IMVCFacade> m_instanceMap;
    //}

    //public class MVCFacade
    //{
    //    #region Model
    //    Dictionary<string, IModel> Models = new Dictionary<string, IModel>();
    //    public void RegistModel(IModel model)
    //    {
    //        if (Models.ContainsKey(model.ModelName))
    //        {
    //            UnRegistModel(model.ModelName);
    //        }
    //        Models.Add(model.ModelName, model);
    //    }

    //    public void UnRegistModel(string modelName)
    //    {
    //        Models.Remove(modelName);
    //    }

    //    public IModel GetModel(string modelName)
    //    {
    //        if (Models.ContainsKey(modelName))
    //        {
    //            return Models[modelName];
    //        }
    //        else
    //        {
    //            return null;
    //        }
    //    }
    //    #endregion

    //    public void DoCommand(string commandName, object[] parameters)
    //    {
    //        IControllerManager control = BasicFacade.Instence.GetInstence<IControllerManager>(commandName);
    //        control.Execute(parameters);
    //    }
    //}
}