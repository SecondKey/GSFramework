using System;
using System.Collections.Generic;

namespace GSFramework.MVC
{
    public class MacroCommand : ICommand
    {
        //public MacroCommand()
        //{
        //    m_subCommands = new List<object>();
        //    InitializeMacroCommand();
        //}
        //public MacroCommand(IEnumerable<Type> types)
        //{
        //    m_subCommands = new List<object>(types);
        //    InitializeMacroCommand();
        //}
        //public MacroCommand(IEnumerable<ICommand> commands)
        //{
        //    m_subCommands = new List<object>(commands);
        //    InitializeMacroCommand();
        //}
        //public MacroCommand(IEnumerable<object> commandCollection)
        //{
        //    m_subCommands = new List<object>(commandCollection);
        //    InitializeMacroCommand();
        //}

        //public void Execute(INotification notification)
        //{
        //    while (m_subCommands.Count > 0)
        //    {
        //        var commandType = m_subCommands[0] as Type;
        //        if (commandType != null)
        //        {
        //            var commandInstance = Activator.CreateInstance(commandType);

        //            if (commandInstance is ICommand)
        //            {
        //                var command = (ICommand)commandInstance;
        //                command.InitializeNotifier(MultitonKey);
        //                command.Execute(notification);
        //            }
        //        }
        //        else
        //        {
        //            var command = m_subCommands[0] as ICommand;
        //            if (command != null)
        //            {
        //                command.InitializeNotifier(MultitonKey);
        //                command.Execute(notification);
        //            }
        //        }

        //        m_subCommands.RemoveAt(0);
        //    }
        //}


        //protected virtual void InitializeMacroCommand()
        //{
        //}
        //protected void AddSubCommand(Type commandType)
        //{
        //    m_subCommands.Add(commandType);
        //}
        //protected void AddSubCommand(ICommand command)
        //{
        //    m_subCommands.Add(command);
        //}
        //private readonly IList<object> m_subCommands;

        public void Execute(params object[] parameters)
        {
            throw new NotImplementedException();
        }

    }
}
