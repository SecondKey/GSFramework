using System;


namespace GSFramework.MVC
{
    public class DelegateCommand : ICommand
    {
        //public DelegateCommand(Action<INotification> action)
        //{
        //    m_action = action;
        //}

        //public virtual void Execute(INotification notification)
        //{
        //    m_action(notification);
        //}

        //private readonly Action<INotification> m_action;
        public void Execute(params object[] parameters)
        {
            throw new NotImplementedException();
        }
    }
}
